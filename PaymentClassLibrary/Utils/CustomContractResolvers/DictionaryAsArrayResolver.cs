using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PaymentClassLibrary.Utils
{
    /// <summary>
    /// Кастомное правило десериализации: необходим для того, чтобы в тексте Json, внутренний объект был десериализован в Dictionary
    /// </summary>
    public class DictionaryAsArrayResolver : DefaultContractResolver
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="objectType"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        protected override JsonContract CreateContract(Type objectType)
        {
            if (objectType.GetInterfaces().Any(i => i == typeof(IDictionary) ||
                (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>))))
            {
                return base.CreateArrayContract(objectType);
            }

            return base.CreateContract(objectType);
        }
    }
}
