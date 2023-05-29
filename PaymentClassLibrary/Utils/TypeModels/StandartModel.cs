using PaymentClassLibrary.Signature;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.Utils.TypeModels
{
    /// <summary>
    /// Стандартная модель, используется во всех остальных случаях, кроме кастомных определенных для Тинькофф и SBC
    /// </summary>
    public class StandartModel : ITypeModel
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
                             .Where(i =>
                                     i.GetCustomAttributes(typeof(DisplayAttribute), true).Length != 0
                                     && i.GetValue(model, null) != null
                                     && i.GetCustomAttributes(typeof(OnlySignatureFieldAttribute), true).Length == 0)
                             .ToDictionary(
                                 p => p.GetCustomAttributes(typeof(DisplayAttribute), true)
                                    .Cast<DisplayAttribute>()
                                    .Single().Name,
                                 p => p.GetValue(model, null).ToString()
                             );

            return result;
        }
    }
}
