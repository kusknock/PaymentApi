using PaymentClassLibrary.SignatureMethods;
using PaymentClassLibrary.SignatureModels;
using PaymentClassLibrary.Utils;
using System.Collections.Generic;

namespace PaymentClassLibrary.Signature.SignatureCreators
{

    /// <summary>
    /// Создание подписи для запроса OAuth
    /// </summary>
    public class OAuthSignatureCreator : ISignatureCreator
    {
        private ISignatureMethod signatureMethod;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="signatureMethod">Метод шифрования</param>
        public OAuthSignatureCreator(ISignatureMethod signatureMethod)
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

            var oauthSignature = new OAuthSignature(signatureMethod.GetSignature(signature));

            return oauthSignature;
        }
    }
}
