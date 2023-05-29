using PaymentClassLibrary.SignatureMethods;
using PaymentClassLibrary.SignatureModels;
using PaymentClassLibrary.Utils;
using System.Collections.Generic;

namespace PaymentClassLibrary.Signature.SignatureCreators
{

    /// <summary>
    /// Создание подписи для SBC
    /// </summary>
    public class AlfaBankSignatureCreator : ISignatureCreator
    {
        private ISignatureMethod signatureMethod;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="signatureMethod">Метод шифрования</param>
        public AlfaBankSignatureCreator(ISignatureMethod signatureMethod)
        {
            this.signatureMethod = signatureMethod;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="data"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IModel CreateSignature(IDictionary<string, string> data)
        {
            var signature = signatureMethod.GetSignatureBaseString(data);

            var alfaBankSignature = new AlfaBankSignature(signatureMethod.GetSignature(signature));

            return alfaBankSignature;
        }
    }
}
