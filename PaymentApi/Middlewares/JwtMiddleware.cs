using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaymentApi.Configuration;
using PaymentApi.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.Middlewares
{
    /// <summary>
    /// ������������� ���������� ��� �������� JWT
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="next"><inheritdoc/></param>
        /// <param name="appSettings">��������� ��� �������� JWT</param>
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// ����� �����������
        /// </summary>
        /// <param name="context">�������� �������</param>
        /// <param name="userService">�������� �� � ������� �������������</param>
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, userService, token);

            await _next(context);
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        /// <param name="context">�������� �������</param>
        /// <param name="userService">�������� �� � ������� �������������</param>
        /// <param name="token">����� �� ����� Authorization</param>
        private async Task AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                // ���������� � �������� ������� �������� User, ���� �� ��� ������ � ��
                context.Items["User"] = await userService.GetById(userId);
            }
            catch
            {
                // TODO: ��� ����� ����������� �������
            }
        }
    }
}