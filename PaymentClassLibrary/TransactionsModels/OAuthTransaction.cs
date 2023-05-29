using PaymentClassLibrary.Normalizer;
using PaymentClassLibrary.OAuth;
using PaymentClassLibrary.Transport.Requests;
using PaymentClassLibrary.Utils;
using PaymentClassLibrary.Utils.TypeModels;
using System;
using System.Collections.Generic;

namespace PaymentClassLibrary.TransactionsModels.AlfaBank
{
    /// <summary>
    /// OAuth-транзакция
    /// </summary>
    public class OAuthTransaction : IPaymentTransaction
    {
        private IModel data;

        private string apiUrl;

        private DataBrowser dataBrowser;
        private readonly IOAuth oAuth;
        private readonly IParametersNormalizer parametersNormalizer;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string ApiUrl => apiUrl;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public OAuthTransaction() { }

        /// <summary>
        /// Конструктор с данными, реализацией OAuth-авторизации и нормализатором параметров
        /// </summary>
        /// <param name="data">Данные</param>
        /// <param name="oAuth">Реализация интерфейса OAuth</param>
        /// <param name="parametersNormalizer">Нормализатор параметров</param>
        public OAuthTransaction(IModel data,
            IOAuth oAuth,
            IParametersNormalizer parametersNormalizer)
        {

            this.data = data;

            this.oAuth = oAuth;
            this.parametersNormalizer = parametersNormalizer;


            dataBrowser = new DataBrowser(
                new List<IModel>
                {
                    this.data,
                }
            );
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
        /// <returns><inheritdoc/></returns>
        public IDictionary<string, string> GetSignatureData()
        {
            return oAuth.GetOAuthData(dataBrowser.GetData(new StandartModel()));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public IRequest GetRequest()
        {
            return new OAuthRequest(dataBrowser.GetData(new StandartModel()), oAuth, parametersNormalizer);
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


