using PaymentClassLibrary.Signature.SignatureCreators;
using PaymentClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.CallbackValidators
{
    /// <summary>
    /// Валидатор Тинькоффских нотификаций при оплате
    /// </summary>
    public class TinkoffNotificationValidator : ICallbackValidator
    {
        private readonly ISignatureCreator signatureCreator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="signatureCreator">Создатель (ля, как пафосно) подписи</param>
        public TinkoffNotificationValidator(ISignatureCreator signatureCreator)
        {
            this.signatureCreator = signatureCreator;
        }

        /// <summary>
        /// Метод валидации подписи
        /// </summary>
        /// <param name="typeModel">Тип модели данных, от нее зависит способ получения данных</param>
        /// <param name="data">Данные из которых будет создана подпись для сравнения с полученной подписью</param>
        /// <param name="signature">Полученная подпись от источника</param>
        /// <returns>Валидна или нет</returns>
        public bool ValidateSignature(ITypeModel typeModel, IModel data, IModel signature)
        {
            var _signature = signatureCreator.CreateSignature(typeModel.GetData(data));

            return _signature.Equals(signature);
        }
    }
}
