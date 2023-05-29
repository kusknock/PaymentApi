using System;
using System.IO;

namespace PaymentApi.Configuration
{
    /// <summary>
    /// Конфигуратор путей в директории приложения
    /// <br/>Пример <see cref="AlfaBankSettings.PathPrivateKey"/>
    /// </summary>
    public class PathConfigurator
    {
        /// <summary>
        /// Переменная пути
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Instance фабрики для конструирования пути
        /// </summary>
        public readonly static PathFactory Factory = new();

        /// <summary>
        /// Приватный конструктор для создания объекта пути только через фабрику
        /// </summary>
        /// <param name="path"></param>
        private PathConfigurator(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Фабрика для конструирования пути
        /// <br/>Пример использования: <see cref="AlfaBankSettings.pathConfig"/>
        /// </summary>
        public class PathFactory
        {
            /// <summary>
            /// Получение пути от корневой директории на сервере
            /// </summary>
            /// <param name="PartOfPath">Относительный от корневой директории путь</param>
            /// <returns>Объект конфигуратора пути</returns>
            public PathConfigurator GetPathCurrentDomain(string PartOfPath = "")
            {
                string resultPath = string.Format("{0}{1}", Directory.GetCurrentDirectory(), PartOfPath);

                return new PathConfigurator(resultPath);
            }
        }
    }
}