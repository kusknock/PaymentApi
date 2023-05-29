using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentClassLibrary.OAuth
{
    /// <summary>
    /// Собственная реализация подготовки данных для OAuth запроса
    /// </summary>
    public class MiniOAuth1 : IOAuth
    {
        private string timeStamp, nonce, consumerKey, signatureMethodName;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Version => "1.0";

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="consumerKey">Логин SBC</param>
        /// <param name="signatureMethodName">Способ подписывания запроса</param>
        public MiniOAuth1(string consumerKey, string signatureMethodName)
        {
            this.consumerKey = consumerKey;
            this.signatureMethodName = signatureMethodName;
        }

        /// <summary>
        /// Установка временной метки
        /// </summary>
        /// <param name="date">Временная метка</param>
        /// <returns>Объект с проставленной временной меткой</returns>
        public MiniOAuth1 SetTimeStamp(DateTime date = default)
        {
            if (date == default)
                date = DateTime.UtcNow;

            var dateDiff = date - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            timeStamp = Convert.ToInt64(dateDiff.TotalSeconds).ToString();

            return this;
        }

        /// <summary>
        /// Случайная фраза
        /// </summary>
        /// <param name="nonce">Случайная фраза (устанавливается с помощью <see cref="Utils.Extensions.RandomString(int)"/></param>
        /// <returns>Объект с инициализированной случайной фразой</returns>
        public MiniOAuth1 SetNonce(string nonce)
        {
            this.nonce = nonce;

            return this;
        }

        /// <summary>
        /// Формирование параметров с учетом полей для OAuth
        /// </summary>
        /// <param name="signature">Строка с подписью</param>
        /// <returns>Набор ключ-значений</returns>
        private IDictionary<string, string> GetOAuthParameters(string signature = null)
        {
            var oAuthParameters = new SortedDictionary<string, string>
                {
                    { "oauth_consumer_key", consumerKey },
                    { "oauth_nonce", nonce },
                    { "oauth_timestamp", timeStamp },
                    { "oauth_signature_method", signatureMethodName },
                    { "oauth_version", Version }
                };

            if (signature != null)
                oAuthParameters.Add("oauth_signature", signature);

            return oAuthParameters;
        }

        /// <summary>
        /// Данные для тела запроса без подписи
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IDictionary<string, string> GetOAuthData(IDictionary<string, string> data)
        {
            var requestParameters = GetOAuthParameters();

            foreach (var item in data)
                requestParameters.Add(item.Key, item.Value);

            return requestParameters;
        }


        /// <summary>
        /// Данные для заголовка запроса (Authorization) с подписью
        /// </summary>
        /// <param name="signature"></param>
        /// <returns></returns>
        public string GetOAuthHeader(string signature)
        {
            var builder = new StringBuilder("OAuth realm=\"\"");

            var oauthParams = GetOAuthParameters(signature: signature);

            foreach (var pair in oauthParams)
            {
                if (builder.Length > 0)
                {
                    builder.Append(',');
                }

                builder.Append(string.Format("{0}=\"{1}\"", Uri.EscapeDataString(pair.Key), Uri.EscapeDataString(pair.Value)));
            }

            return builder.ToString();
        }
    }
}
