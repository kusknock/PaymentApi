using PaymentClassLibrary.SignatureMethods;
using PaymentClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentClassLibrary.Signature.SignatureCreators
{
    /// <summary>
    /// Интерфейс объекта, создающего подпись из модели
    /// </summary>
    public interface ISignatureCreator
    {
        /// <summary>
        /// Метод обрабатывающий данные и создающий подпись
        /// </summary>
        /// <param name="data">Ключи и значения для создания подписи</param>
        /// <returns>Модель подписи</returns>
        IModel CreateSignature(IDictionary<string, string> data);
    }
}
