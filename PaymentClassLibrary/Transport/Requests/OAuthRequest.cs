using PaymentClassLibrary.Normalizer;
using PaymentClassLibrary.OAuth;
using PaymentClassLibrary.Utils;
using PaymentClassLibrary.Utils.TypeModels;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace PaymentClassLibrary.Transport.Requests
{
    /// <summary>
    /// Класс для формирования данных для запроса OAuth
    /// </summary>
    public class OAuthRequest : IRequest
    {
        private readonly SortedDictionary<string, string> requestFields;
        private readonly IOAuth miniOAuth;
        private readonly IParametersNormalizer parametersNormalizer;
        private string apiUrl, signature, contentType;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="requestFields">Данные для запроса</param>
        /// <param name="miniOAuth">Объект OAuth</param>
        /// <param name="parametersNormalizer">Нормализатор для параметров</param>
        /// <param name="contentType">Тип контента (по умолчанию UrlEncoded)</param>
        public OAuthRequest(IDictionary<string, string> requestFields,
                            IOAuth miniOAuth,
                            IParametersNormalizer parametersNormalizer,
                            string contentType = UserMimeType.UrlEncoded)
        {
            this.requestFields = new SortedDictionary<string, string>(requestFields);
            this.miniOAuth = miniOAuth;
            this.parametersNormalizer = parametersNormalizer;
            this.contentType = contentType;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public string GetApiUrl() => apiUrl;

        /// <summary>
        /// //Параметры OAuth, без подписи со всеми полями данных
        /// </summary>
        /// <returns></returns>
        public string GetRequestDataBody()
        {
            var oAuthParams = miniOAuth.GetOAuthData(requestFields);

            return parametersNormalizer.GetNormalizedParameters(oAuthParams);
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
        /// <returns><inheritdoc/></returns>
        public NameValueCollection GetRequestHeader()
        {
            return new NameValueCollection
            {
                { "Authorization", miniOAuth.GetOAuthHeader(signature) }
            };
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="signature"><inheritdoc/></param>
        public void SetSignature(IModel signature)
        {
            var dataBrowser = new StandartModel();

            this.signature = dataBrowser.GetData(signature)["signature"];
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public string GetContentType()
        {
            return contentType;
        }
    }
}