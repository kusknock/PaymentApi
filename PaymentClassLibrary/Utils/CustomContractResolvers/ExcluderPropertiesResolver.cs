using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.Utils.CustomContractResolvers
{

    /// <summary>
    /// Кастомное правило для исключения некоторых полей с определенным атрибутом при сериализации или десериализации Json
    /// </summary>
    /// <typeparam name="T">Тип аттрибута</typeparam>
    public class ExcluderPropertiesResolver<T> : DefaultContractResolver where T : Attribute
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ExcluderPropertiesResolver()
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="objectType"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            return base.GetSerializableMembers(objectType)
              .Where(mi => mi.GetCustomAttribute<T>() == null)
              .ToList();
        }
    }
}
