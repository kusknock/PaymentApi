namespace PaymentApi.Models
{
    /// <summary>
    /// Модель IP из белого списка
    /// </summary>
    public class IpAddress
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// IP-адрес
        /// </summary>
        public string Host { get; set; }
    }
}