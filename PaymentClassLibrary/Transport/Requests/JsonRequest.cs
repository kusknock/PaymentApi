using PaymentClassLibrary.Utils;
using PaymentClassLibrary.Utils.TypeModels;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;

namespace PaymentClassLibrary.Transport.Requests
{
    /// <summary>
    /// Запрос с данными в виде Json
    /// </summary>
    public class JsonRequest : IRequest
    {
        private RequestData requestData;
        private string apiUrl;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="requestData"></param>
        public JsonRequest(RequestData requestData)
        {
            this.requestData = requestData;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public string GetApiUrl() => apiUrl;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public string GetContentType()
        {
            return requestData.ContentType;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public string GetRequestDataBody()
        {
            return requestData.RequestString;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public NameValueCollection GetRequestHeader()
        {
            return new NameValueCollection();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="apiUrl"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IRequest SetApiUrl(string apiUrl)
        {
            this.apiUrl = apiUrl;

            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="signature"><inheritdoc/></param>
        public void SetSignature(IModel signature)
        {
            JObject jobj = RequestData.Deserializer.DeserializeJson(requestData);

            var dataBrowser = new StandartModel();

            foreach (var item in dataBrowser.GetData(signature))
            {
                jobj.Add(item.Key, item.Value);
            }

            requestData = RequestData.Serializer.SerializeJsonData(jobj);
        }
    }
}