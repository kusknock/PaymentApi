using System;

namespace PaymentClassLibrary.Signature
{
    /// <summary>
    /// Атрибут для сортировки полей при формировании подписи в алгоритмах SBC
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class SignatureAttribute : Attribute
    {
        /// <summary>
        /// Номер поля по порядку
        /// </summary>
        public int FieldOrder { get; private set; }

        /// <summary>
        /// Конструктор с номером поля подписи по порядку
        /// </summary>
        /// <param name="fieldOrder">Порядковый номер</param>
        public SignatureAttribute(int fieldOrder)
        {
            FieldOrder = fieldOrder;
        }
    }
}
