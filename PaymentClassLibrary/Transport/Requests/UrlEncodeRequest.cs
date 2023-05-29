using PaymentClassLibrary.Utils;
using PaymentClassLibrary.Utils.TypeModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace PaymentClassLibrary.Transport.Requests
{
    /// <summary>
    /// Реализация UrlEncoded запроса
    /// </summary>
    public class UrlEncodeRequest : IRequest
    {
        private RequestData requestData;

        private string apiUrl;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="requestData"></param>
        public UrlEncodeRequest(RequestData requestData)
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
            var dict = new SortedDictionary<string,string>(RequestData.Deserializer.DeserializeUrlEncodedString(requestData));

            var dataBrowser = new StandartModel();

            foreach (var item in dataBrowser.GetData(signature))
            {
                dict.Add(item.Key, item.Value);
            }

            requestData = RequestData.Serializer.SerializeUrlEncodedData(dict);
        }
    }
}