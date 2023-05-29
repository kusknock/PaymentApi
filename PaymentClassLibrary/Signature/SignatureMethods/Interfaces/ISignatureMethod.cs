using PaymentClassLibrary.SignatureCollectors;

namespace PaymentClassLibrary.SignatureMethods
{
    /// <summary>
    /// Интерфейс методов шифрования
    /// </summary>
    public interface ISignatureMethod : ICollectSignatureString
    {
        /// <summary>
        /// Название метода
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Получение подписи
        /// </summary>
        /// <param name="signatureBaseString">Базовая строка из которой будет создаваться подпись</param>
        /// <returns>Подпись в виде строки</returns>
        string GetSignature(string signatureBaseString);
    }
}