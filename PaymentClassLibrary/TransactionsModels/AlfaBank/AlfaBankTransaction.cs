using PaymentClassLibrary.Transport.Requests;
using PaymentClassLibrary.Utils;
using PaymentClassLibrary.Utils.TypeModels;
using System.Collections.Generic;

namespace PaymentClassLibrary.TransactionsModels.AlfaBank
{
    /// <summary>
    /// Данные для вызова методов платежного сервиса SBC
    /// </summary>
    public class AlfaBankTransaction : IPaymentTransaction
    {
        private IModel data;

        private string apiUrl;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="data">Данные для запроса</param>
        public AlfaBankTransaction(IModel data = null)
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
            var requestData = RequestData.Serializer.SerializeUrlEncodedData(data, new StandartModel());

            return new UrlEncodeRequest(requestData);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public IDictionary<string, string> GetSignatureData()
        {
            var dataBrowser = new AlfaBankSignatureDefinition();

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