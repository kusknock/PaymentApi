using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentClassLibrary.TransactionsModels.AlfaBank
{
    /// <summary>
    /// Список методов и фабрика для их формирования 
    /// </summary>
    public class AlfaBankMethod
    {
        /// <summary>
        /// Api метод
        /// </summary>
        public string ApiMethod { get; init; }
        /// <summary>
        /// Идентификатор терминала
        /// </summary>
        public string EndPointId { get; init; }
        /// <summary>
        /// Фабрика методов
        /// </summary>
        public static AlfaBankMethods Methods = new AlfaBankMethods();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="apiMethod">Название метода</param>
        /// <param name="endPointId">Идентификатор терминала</param>
        private AlfaBankMethod(string apiMethod, string endPointId)
        {
            ApiMethod = apiMethod;
            EndPointId = endPointId;
        }

        /// <summary>
        /// Класс фабрики методов
        /// </summary>
        public class AlfaBankMethods
        {
            /// <summary>
            /// Предварительная аутентификация
            /// </summary>
            public AlfaBankMethod Preauth { get { return new AlfaBankMethod("preauth-form", "4585"); } }
            /// <summary>
            /// Привязка карты
            /// </summary>
            public AlfaBankMethod CardRegistration { get { return new AlfaBankMethod("create-card-ref", "4585"); } }
            /// <summary>
            /// Отмена транзакции (возврат средств)
            /// </summary>
            public AlfaBankMethod ReturnTransaction { get { return new AlfaBankMethod("return", "4585"); } }
            /// <summary>
            /// Выплата
            /// </summary>
            public AlfaBankMethod TransferMoney { get { return new AlfaBankMethod("transfer-by-ref", "4586"); } }
            /// <summary>
            /// Реккурентное списание
            /// </summary>
            public AlfaBankMethod ReccurentPayment { get { return new AlfaBankMethod("make-rebill", "4587"); } }
            /// <summary>
            /// Оплата через платежную форму
            /// </summary>
            public AlfaBankMethod SaleForm { get { return new AlfaBankMethod("sale-form", "4584"); } }

            /// <summary>
            /// Метод для формирования транзакции статуса.
            /// В данном случае необходимо, чтобы терминал на котором выполнялась транзакция, 
            /// статус которой необходимо запросить совпадал в транзакции статуса
            /// </summary>
            /// <param name="method">Объект метода, статус которого необходимо запросить</param>
            /// <returns>Метод статуса с определенным идентификатором терминала</returns>
            public AlfaBankMethod StatusTransaction(AlfaBankMethod method)
            {
                return new AlfaBankMethod("status", method.EndPointId);
            }

            /// <summary>
            /// Если короче, статус должен создаваться от той транзакции, для которой мы должны запросить статус
            /// </summary>
            /// <param name="apiMethod">Название метода, для которой необходимо запросить статус</param>
            /// <returns>Метод статуса с определенным идентификатором терминала</returns>
            public AlfaBankMethod StatusTransaction(string apiMethod)
            {
                var method = GetInstanceByMethodName(apiMethod);

                return StatusTransaction(method);
            }

            /// <summary>
            /// Получение объекта метода по названию метода транзакции
            /// </summary>
            /// <param name="apiMethod">Название метода</param>
            /// <returns>Метод транзакции с названием и терминалом</returns>
            private AlfaBankMethod GetInstanceByMethodName(string apiMethod)
            {
                switch (apiMethod)
                {
                    case "preauth-form": return Preauth;
                    case "create-card-ref": return CardRegistration;
                    case "return": return ReturnTransaction;
                    case "transfer-by-ref": return TransferMoney;
                    case "make-rebill": return ReccurentPayment;
                    case "sale-form": return SaleForm;
                    default: return null;
                }
            }
        }
    }



}
