using PaymentClassLibrary.Utils;

namespace PaymentClassLibrary.CallbackValidators
{
    /// <summary>
    /// Валидатор нотификаций/callbacks
    /// </summary>
    public interface ICallbackValidator
    {
        /// <summary>
        /// Метод валидации подписи нотификации
        /// </summary>
        /// <param name="typeModel"></param>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        bool ValidateSignature(ITypeModel typeModel, IModel data, IModel signature);
    }
}