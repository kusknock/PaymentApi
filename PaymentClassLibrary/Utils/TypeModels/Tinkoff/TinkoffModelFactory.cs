using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentClassLibrary.Utils.TypeModels.Tinkoff
{
    /// <summary>
    /// Фабрика для получения модели определенной версии
    /// </summary>
    public class TinkoffModelFactory
    {
        /// <summary>
        /// Получение модели по версии
        /// </summary>
        /// <param name="Version">Версия</param>
        /// <returns>Тип модели</returns>
        public static ITypeModel GetTinkoffModelByVersion(int Version)
        {
            switch (Version)
            {
                case 0:
                    return new TinkoffModel();
                case 2:
                    return new TinkoffV2Model();
                default:
                    return null;
            }
        }
    }
}
