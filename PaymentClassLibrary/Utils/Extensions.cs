using PaymentClassLibrary.Signature;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Web;

namespace PaymentClassLibrary.Utils
{
    /// <summary>
    /// Некоторые расширения
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Генерация случайной строки
        /// </summary>
        /// <param name="length">Длина</param>
        /// <returns>Случайная строка</returns>
        public static string RandomString(int length)
        {
            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Получение из Dictionary NameValueCollection
        /// </summary>
        /// <typeparam name="TKey">Тип ключ</typeparam>
        /// <typeparam name="TValue">Тип значение</typeparam>
        /// <param name="dict">Объект Dictionary</param>
        /// <returns>Объект NameValueCollection</returns>
        public static NameValueCollection ToNameValueCollection<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            var nameValueCollection = new NameValueCollection();

            foreach (var kvp in dict)
            {
                string value = null;
                if (kvp.Value != null)
                    value = kvp.Value.ToString();

                nameValueCollection.Add(kvp.Key.ToString(), value);
            }

            return nameValueCollection;
        }

        /// <summary>
        /// Получение из NameValueCollection SortedDictionary
        /// </summary>
        /// <param name="nvc">Объект NameValueCollection</param>
        /// <returns>SortedDictionary</returns>
        public static IDictionary<string, string> ToSortedDictionary(this NameValueCollection nvc)
        {
            return new SortedDictionary<string, string>(nvc.AllKeys.ToDictionary(k => k, k => nvc[k]));
        }

        /// <summary>
        /// Получение из NameValueCollection Dictionary
        /// </summary>
        /// <param name="nvc">Объект NameValueCollection</param>
        /// <returns>Dictionary</returns>
        public static IDictionary<string, string> ToDictionary(this NameValueCollection nvc)
        {
            return nvc.AllKeys.ToDictionary(k => k, k => nvc[k]);
        }

        /// <summary>
        /// Из NameValueCollection QueryString (обратная операция от <see cref="HttpUtility.ParseQueryString(string)"/>)
        /// </summary>
        /// <param name="nvc">Объект NameValueCollection</param>
        /// <returns>Строка UrlEncoded</returns>
        public static string ToQueryString(this NameValueCollection nvc)
        {
            if (nvc == null) return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (string key in nvc.Keys)
            {
                if (string.IsNullOrWhiteSpace(key)) continue;

                string[] values = nvc.GetValues(key);
                if (values == null) continue;

                foreach (string value in values)
                {
                    if (sb.Length > 0)
                        sb.Append("&");

                    sb.AppendFormat("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));
                }
            }

            return sb.ToString();
        }
    }
}
