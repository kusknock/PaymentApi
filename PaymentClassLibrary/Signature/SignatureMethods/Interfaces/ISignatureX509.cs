namespace PaymentClassLibrary.SignatureMethods
{
    /// <summary>
    /// Интерфейс методов шифрования с использования X509 сертификатов
    /// </summary>
    public interface ISignatureX509 :ISignatureMethod
    {
        /// <summary>
        /// Серийный номер сертификата
        /// </summary>
        public string CertSerial { get; }
    }
}