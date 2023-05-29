using PaymentClassLibrary.SignatureCollectors;
using PaymentClassLibrary.SignatureMethods;
using PaymentClassLibrary.SignatureModels;
using PaymentClassLibrary.Utils;
using System.Collections.Generic;

namespace PaymentClassLibrary.Signature.SignatureCreators
{

    /// <summary>
    /// Создание подписи для алгоритмов E2C протокола Тинькофф
    /// </summary>
    public class TinkoffBindSignatureCreator : ISignatureCreator
    {
        private readonly ISignatureMethod signatureMethod;
        private readonly ISignatureMethod hashMethod;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="signatureMethod">Метод шифрования</param>
        /// <param name="hashMethod">Метод хэширования</param>
        public TinkoffBindSignatureCreator(ISignatureMethod signatureMethod, ISignatureMethod hashMethod = null)
        {
            this.signatureMethod = signatureMethod;
            this.hashMethod = hashMethod ?? new Sha256(new SignatureCollectorWithSort());
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="data"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IModel CreateSignature(IDictionary<string, string> data)
        {
            var baseString = hashMethod.GetSignatureBaseString(data);

            var digestValue = hashMethod.GetSignature(baseString);

            var signature = signatureMethod.GetSignature(digestValue);

            var tinkoffSignature = new TinkoffBindSignature(digestValue, signature, signatureMethod);

            return tinkoffSignature;
        }
    }
}
