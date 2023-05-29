using PaymentClassLibrary.TransactionsModels.Tinkoff;
using PaymentClassLibrary.Transport.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.Signature.SignatureAttributes
{
    /// <summary>
    /// Аттрибут для игнорирования полей для подписи при сериализации через Json c ContractResolver'ом.
    /// Надо для того, чтобы можно было сериализовать объект как с нужными полями, так и без них
    /// <br/>Например <see cref="InitRebillData.Password"/>
    /// <br/>Разрешается с помощью <see cref="RequestData.RequestDataSerializer.SerializeJsonData(Utils.IModel, Newtonsoft.Json.Serialization.DefaultContractResolver)"/>
    /// <br/>Пример <see cref="TinkoffBankV2Transaction.GetRequest"/>
    /// </summary>
    public class JsonIgnoreForSignature : Attribute
    {
    }
}
