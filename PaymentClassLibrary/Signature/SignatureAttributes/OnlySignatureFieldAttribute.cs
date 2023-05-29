using System;

namespace PaymentClassLibrary.Signature
{ 
    /// <summary>
    /// Атрибут поля, который игнорируется при формирования подписи
    /// </summary>
    public class OnlySignatureFieldAttribute : Attribute
    {
    }
}