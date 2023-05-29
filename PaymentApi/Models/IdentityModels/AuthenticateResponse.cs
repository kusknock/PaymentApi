using Microsoft.AspNetCore.Identity;
using PaymentApi.Models.IdentityModels;
using System.Collections.Generic;

namespace PaymentApi.Models
{
    /// <summary>
    /// ����� ����� ��������������
    /// </summary>
    public class AuthenticateResponse : IIdentityResponse
    {
        /// <summary>
        /// �������������
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ��� ������������
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// ����� JWT
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<IdentityError> Errors { get; private set; }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="user">������ ������������</param>
        public AuthenticateResponse(User user)
        {
            Id = user.Id;
            Username = user.UserName;
            Token = user.JwtToken;
            Errors = null;
        }

        /// <summary>
        /// ����������� � ��������
        /// </summary>
        /// <param name="model">������ �������</param>
        /// <param name="errors">���������� ������</param>
        public AuthenticateResponse(AuthenticateRequest model, IEnumerable<IdentityError> errors)
        {
            Id = "-1";
            Username = model.UserName;
            Token = null;
            Errors = errors;
        }
    }
}