using PaymentClassLibrary.Signature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.Utils.TypeModels
{
    /// <summary>
    /// Модель Тинькофф второй версии (v2)
    /// </summary>
    public class TinkoffV2Model : ITypeModel
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="model"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public IDictionary<string, string> GetData(IModel model)
        {
            var result = model.GetType()
                 .GetProperties()
                 .Where(i => i.GetValue(model, null) != null
                         && i.GetCustomAttributes(typeof(OnlySignatureFieldAttribute), true).Length == 0)
                 .ToDictionary(
                     p => p.Name,
                     p => p.GetValue(model, null).ToString()
                 );

            return result;
        }
    }
}
