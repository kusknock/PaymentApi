using PaymentClassLibrary.SignatureMethods;
using PaymentClassLibrary.SignatureModels;
using PaymentClassLibrary.Utils;
using System.Collections.Generic;

namespace PaymentClassLibrary.Signature.SignatureCreators
{
    /// <summary>
    /// Создание подписи для алгоритмов оплат Tinkoff
    /// </summary>
    public class TinkoffPaySignatureCreator : ISignatureCreator
    {
        private ISignatureMethod signatureMethod;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="signatureMethod">Метод шифрования</param>
        public TinkoffPaySignatureCreator(ISignatureMethod signatureMethod)
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

            var tinkoffSignature = new TinkoffPaySignature(signatureMethod.GetSignature(signature));

            return tinkoffSignature;
        }
    }
}
