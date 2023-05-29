using PaymentClassLibrary.Signature.SignatureCreators;
using PaymentClassLibrary.SignatureMethods;
using PaymentClassLibrary.TransactionsModels;
using PaymentClassLibrary.Transport;
using PaymentClassLibrary.Transport.Requests;
using PaymentClassLibrary.Transport.Responses;
using PaymentClassLibrary.Utils;

namespace PaymentClassLibrary.Queries
{
    /// <summary>
    /// Прототип запроса
    /// </summary>
    public abstract class Query
    {
        /// <summary>
        /// Объект для формирования подписи
        /// </summary>
        protected ISignatureCreator signatureCreator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="signatureCreator">Объект для формирования подписи</param>
        public Query(ISignatureCreator signatureCreator)
        {
            this.signatureCreator = signatureCreator;
        }

        /// <summary>
        /// Создание объекта запроса для отправки через <see cref="GatewayClient"/>
        /// </summary>
        /// <param name="paymentTransaction">Модель транзакции</param>
        /// <returns></returns>
        public virtual IRequest CreateRequest(IPaymentTransaction paymentTransaction)
        {
            IRequest request = PaymentTransactionToRequest(paymentTransaction)
                .SetApiUrl(paymentTransaction.ApiUrl);

            request.SetSignature(CreateSignature(paymentTransaction));

            return request;
        }

        /// <summary>
        /// Метод для обработки модели транзакции в модель запроса
        /// </summary>
        /// <param name="paymentTransaction"></param>
        /// <returns></returns>
        protected abstract IRequest PaymentTransactionToRequest(IPaymentTransaction paymentTransaction);

        /// <summary>
        /// Создание подписи
        /// </summary>
        /// <param name="paymentTransaction">Модель транзакции</param>
        /// <returns>Модель данных с подписью, которая присоединяется к запросу</returns>
        protected virtual IModel CreateSignature(IPaymentTransaction paymentTransaction)
        {
            return signatureCreator.CreateSignature(paymentTransaction.GetSignatureData());
        }
    }
}
