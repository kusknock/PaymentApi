using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaymentApi.Models;
using PaymentApi.Models.IdentityModels;
using PaymentApi.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.Services
{

    /// <summary>
    /// ����������� ��� ������ �������������� �� ��
    /// </summary>
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        private readonly UserManager<User> _userManager;

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="appSettings">��������� ���������� ��� �������� ������</param>
        /// <param name="userManager">�������� ��</param>
        public UserService(IOptions<AppSettings> appSettings, UserManager<User> userManager)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="model"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            // ��������� ������������� ������������
            if (user == null)
                return ErrorResponse(model, IdentityTypeErrors.UserNotFound, "User not found");

            var isPassValid = await _userManager.CheckPasswordAsync(user, model.Password);

            // ��������� ��������� �� �� ���� ������
            if (!isPassValid)
                return ErrorResponse(model, IdentityTypeErrors.InvalidUserNameOrPassword, "Invalid UserName or Password");

            // ���������� ����� ���������� ��� � ������
            user.JwtToken = GenerateJwtToken(user);

            var resultUpdate = await _userManager.UpdateAsync(user);

            // ��������� ���������� ����������
            if (!resultUpdate.Succeeded)
                return new AuthenticateResponse(model, resultUpdate.Errors);

            return new AuthenticateResponse(user);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<User> GetAll()
        {
            return _userManager.Users.ToList();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<User> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        /// <summary>
        /// ������������ ���������� ������
        /// </summary>
        /// <param name="model">������ ��������������</param>
        /// <param name="code">��� ������</param>
        /// <param name="description">�������� ������</param>
        /// <returns></returns>
        private static AuthenticateResponse ErrorResponse(AuthenticateRequest model, string code, string description)
        {
           var errors = new List<IdentityError>
                {
                    new IdentityError()
                    {
                        Code = code,
                        Description = description
                    }
                };

            return new AuthenticateResponse(model, errors);
        }

        /// <summary>
        /// ��������� JWT
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateJwtToken(User user)
        {
            // �������� ������ �� 7 ����
            // TODO: ����� ������� ���������� ���� � ���������
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}