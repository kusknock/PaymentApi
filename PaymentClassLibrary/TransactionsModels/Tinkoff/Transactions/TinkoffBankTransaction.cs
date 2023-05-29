using PaymentClassLibrary.Transport.Requests;
using PaymentClassLibrary.Utils;
using PaymentClassLibrary.Utils.TypeModels;
using System;
using System.Collections.Generic;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Первая версия транзакции Тинькофф (Передача UrlEncoded)
    /// </summary>
    public class TinkoffBankTransaction : IPaymentTransaction
    {
        private IModel data;

        private string apiUrl;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="data">Данные</param>
        public TinkoffBankTransaction(IModel data = null)
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
            var requestData = RequestData.Serializer.SerializeUrlEncodedData(data, new TinkoffModel());

            return new UrlEncodeRequest(requestData);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public IDictionary<string, string> GetSignatureData()
        {
            var dataBrowser = new TinkoffModel();

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