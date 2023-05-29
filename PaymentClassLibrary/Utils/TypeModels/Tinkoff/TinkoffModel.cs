using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.Utils.TypeModels
{
    /// <summary>
    /// Модель Тинькофф первой версии, которая десериализуется в UrlEncoded
    /// </summary>
    public class TinkoffModel : ITypeModel
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
                                     && i.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Length == 0)
                             .ToDictionary(
                                 p => GetJsonPropAttribute(model, p.Name) == null ? p.Name : GetJsonPropAttribute(model, p.Name).PropertyName,
                                 p => p.GetValue(model, null).ToString()
                             );

            return result;
        }

        /// <summary>
        /// Получение переопределнное название поля, которое описано в JsonProperty
        /// </summary>
        /// <param name="obj">Объект модели данных</param>
        /// <param name="nameProperty">Название свойства</param>
        /// <returns>Название из JsonProperty</returns>
        private JsonPropertyAttribute GetJsonPropAttribute(object obj, string nameProperty)
        {
            return obj.GetType()
                      .GetMember(nameProperty)
                      .First()
                      .GetCustomAttribute<JsonPropertyAttribute>();
        }
    }
}
