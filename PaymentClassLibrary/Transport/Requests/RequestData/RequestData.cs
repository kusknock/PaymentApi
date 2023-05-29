using PaymentClassLibrary.Utils;
using PaymentClassLibrary.Utils.TypeModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace PaymentClassLibrary.Transport.Requests
{
    /// <summary>
    /// Данные для запроса
    /// </summary>
    public class RequestData
    {
        /// <summary>
        /// Строка с данными
        /// </summary>
        public string RequestString { get; private set; }
        /// <summary>
        /// Тип контента (<see cref="UserMimeType"/>)
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// Сериализатор
        /// </summary>
        public static RequestDataSerializer Serializer = new RequestDataSerializer();

        /// <summary>
        /// Десериализатор
        /// </summary>
        public static RequestDataDeserializer Deserializer = new RequestDataDeserializer();

        /// <summary>
        /// Приватный конструктор для создания объекта только в фабриках
        /// </summary>
        /// <param name="requestString"></param>
        /// <param name="contentType"></param>
        private RequestData(string requestString, string contentType)
        {
            RequestString = requestString;
            ContentType = contentType;
        }

        /// <summary>
        /// Класс фабрики сериализатора
        /// </summary>
        public class RequestDataSerializer
        {
            /// <summary>
            /// Сериализация объекта в UrlEncode
            /// </summary>
            /// <param name="data">Данные</param>
            /// <param name="dataBrowser">Тип модели</param>
            /// <returns>Объект с данными для запроса и типом контента</returns>
            public RequestData SerializeUrlEncodedData(IModel data, ITypeModel dataBrowser)
            {
                return new RequestData(dataBrowser.GetData(data)
                                                  .ToNameValueCollection()
                                                  .ToQueryString(), UserMimeType.UrlEncoded);
            }

            /// <summary>
            /// Сериализация в UrlEncode из Dictionary
            /// </summary>
            /// <param name="data">Данные</param>
            /// <returns>Объект с данными для запроса и типом контента</returns>
            public RequestData SerializeUrlEncodedData(IDictionary<string, string> data)
            {
                return new RequestData(data.ToNameValueCollection().ToQueryString(), UserMimeType.UrlEncoded);
            }

            /// <summary>
            /// Сериализатор Json
            /// </summary>
            /// <param name="data">Данные</param>
            /// <param name="resolver">Кастомные правила для сериализации</param>
            /// <returns>Объект с данными для запроса и типом контента</returns>
            public RequestData SerializeJsonData(IModel data, DefaultContractResolver resolver = null)
            {
                return new RequestData(JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = resolver
                }), UserMimeType.Json);
            }

            /// <summary>
            /// Сериализатор Json из JObject
            /// </summary>
            /// <param name="jobj">Объект JObject</param>
            /// <returns>Объект с данными для запроса и типом контента</returns>
            public RequestData SerializeJsonData(JObject jobj)
            {
                return new RequestData(jobj.ToString(), UserMimeType.Json);
            }
        }

        /// <summary>
        /// Десериализатор
        /// </summary>
        public class RequestDataDeserializer
        {
            /// <summary>
            /// Десериализация Json в JObject
            /// </summary>
            /// <param name="data">Данные запроса</param>
            /// <returns>Объект JObject</returns>
            public JObject DeserializeJson(RequestData data)
            {
                return JObject.Parse(data.RequestString);
            }

            /// <summary>
            /// Десериализация в Dictionary UrlEncode
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public IDictionary<string, string> DeserializeUrlEncodedString(RequestData data)
            {
                return HttpUtility.ParseQueryString(data.RequestString).ToDictionary();
            }
        }
    }
}