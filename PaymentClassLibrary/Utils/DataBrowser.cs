using PaymentClassLibrary.Signature;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentClassLibrary.Utils
{
    /// <summary>
    /// Старый способ сериализации данных в Dictionary (пока используется в OAuth, надо отрефакторить)
    /// </summary>
    public class DataBrowser 
    {
        private List<IModel> data;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="data"></param>
        public DataBrowser(List<IModel> data)
        {
            this.data = data;
        }

        /// <summary>
        /// Сериализация с помощью ITypeModel
        /// </summary>
        /// <param name="typeModel">Тип модели</param>
        /// <returns>Сериализованный Dictionary</returns>
        public IDictionary<string, string> GetData(ITypeModel typeModel)
        {
            var result = new SortedDictionary<string, string>();

            foreach (var browser in data)
            {
                foreach (var item in typeModel.GetData(browser))
                {
                    result.Add(item.Key, item.Value);
                }
            }

            return result;
        }
    }
}
