using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Models;
using PaymentApi.Services;
using PaymentApi.Utils;
using System.Threading.Tasks;

namespace PaymentApi.Controllers
{
    /// <summary>
    /// Контроллер для создания пользователей сервиса
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IRegisterService registerService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userService">Контейнер для получения пользователей и создания токенов</param>
        /// <param name="registerService">Контейнер для регистрации пользователей в системе</param>
        public AccountController(IUserService userService, IRegisterService registerService)
        {
            this.userService = userService;
            this.registerService = registerService;
        }

        /// <summary>
        /// Создание пользователя сервиса. Вызывается только локально либо с определенного IP
        /// <br/>Использование следующее: сами создаем пользователя, после чего отдаем ему данные логин и пароль
        /// </summary>
        /// <param name="model">Данные пользователя</param>
        /// <response code="200">Результат добавления пользователя в БД</response>
        /// <response code="400">Входные данные неверны</response>
        [LocalOnly]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await registerService.Register(model);

            if (response.Errors != null)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        /// <summary>
        /// Аутентификация (создание токена)
        /// </summary>
        /// <param name="model">Данные полученные после регистрации</param>
        /// <response code="200">Возвращается токен JWT и имя пользователя</response>
        /// <response code="400">Входные данные неверны</response>
        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> Auth(AuthenticateRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await userService.Authenticate(model);

            if (response.Errors != null)
                return BadRequest(response.Errors);

            return Ok(response);
        }
    }
}
