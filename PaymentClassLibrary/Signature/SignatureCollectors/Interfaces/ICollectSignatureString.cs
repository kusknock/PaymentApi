using PaymentClassLibrary.Signature.SignatureCreators;
using System.Collections.Generic;

namespace PaymentClassLibrary.SignatureCollectors
{
    /// <summary>
    /// Инструмент для того, чтобы объединять строки при создании подписи
    /// </summary>
    public interface ICollectSignatureString
    {
        /// <summary>
        /// Получение «слепленной» строки из полученных данных
        /// <para>
        /// Пример: <see cref="OAuthSignatureCreator.CreateSignature(IDictionary{string, string})"/>
        /// </para>
        /// </summary>
        /// <param name="data">Данные, которые нужны для объединения</param>
        /// <returns>Объединенная строка</returns>
        string GetSignatureBaseString(IDictionary<string, string> data);
    }
}
