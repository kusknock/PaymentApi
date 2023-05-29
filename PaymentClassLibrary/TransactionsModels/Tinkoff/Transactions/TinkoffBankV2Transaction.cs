using PaymentClassLibrary.Signature.SignatureAttributes;
using PaymentClassLibrary.Transport.Requests;
using PaymentClassLibrary.Utils;
using PaymentClassLibrary.Utils.CustomContractResolvers;
using PaymentClassLibrary.Utils.TypeModels;
using System;
using System.Collections.Generic;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Вторая версия транзакции с передачей параметров Json
    /// </summary>
    public class TinkoffBankV2Transaction : IPaymentTransaction
    {
        private IModel data;

        private string apiUrl;

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="data">Данные для совершения транзакции</param>
        public TinkoffBankV2Transaction(IModel data = null)
        {
            this.data = data;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string ApiUrl => apiUrl;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public IRequest GetRequest()
        {
            var requestData = RequestData.Serializer.SerializeJsonData(data, new ExcluderPropertiesResolver<JsonIgnoreForSignature>());

            return new JsonRequest(requestData);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public IDictionary<string, string> GetSignatureData()
        {
            var dataBrowser = new TinkoffV2Model();

            var signatureDictionary = dataBrowser.GetData(data);

            return signatureDictionary;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="apiUrl"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IPaymentTransaction SetApiUrl(string apiUrl)
        {
            this.apiUrl = apiUrl;

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="data"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IPaymentTransaction SetData(IModel data)
        {
            this.data = data;
            return this;
        }
    }
}