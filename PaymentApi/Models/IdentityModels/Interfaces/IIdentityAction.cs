using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models.IdentityModels
{
    /// <summary>
    /// Интерфейс ответа после регистрации и авторизации
    /// </summary>
    public interface IIdentityResponse
    {
        /// <summary>
        /// Ошибки полученные в результате запроса
        /// </summary>
        IEnumerable<IdentityError> Errors { get; }
    }
}
