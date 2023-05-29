using PaymentClassLibrary.Signature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.Utils.TypeModels
{
    /// <summary>
    /// Модель для подписи SBC
    /// </summary>
    public class AlfaBankSignatureDefinition : ITypeModel
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="model"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IDictionary<string, string> GetData(IModel model)
        {
            var signatureDefinition = new Dictionary<string,string>();

            var items = model.GetType()
                   .GetProperties()
                   .Where(i => i.GetCustomAttributes(typeof(SignatureAttribute), true).Length > 0)
                   .Select(i => new
                   {
                       FieldOrder = i.GetCustomAttributes(typeof(SignatureAttribute), true)
                                .Cast<SignatureAttribute>()
                                .Single().FieldOrder,
                       Name = i.Name,
                       Value = i.GetValue(model, null).ToString()
                   });

            foreach (var item in items)
            {
                var pair = new KeyValuePair<string, string>(item.Name, item.Value);

                signatureDefinition.Add($"{item.FieldOrder}:{item.Name}", item.Value);
            }

            return signatureDefinition;
        }
    }
}
