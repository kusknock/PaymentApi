using System.Collections.Generic;

namespace PaymentClassLibrary.Utils
{
    /// <summary>
    /// Интерфейс типа модели
    /// <br/>По сути способ сериализации модели в Dictionary, чтобы его обработать и «запихать» в запрос
    /// </summary>
    public interface ITypeModel
    {
        /// <summary>
        /// Метод для получения списка ключ-значение для обработки 
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>Сериализованные данные в IDictionary</returns>
        IDictionary<string, string> GetData(IModel model);
    }
}