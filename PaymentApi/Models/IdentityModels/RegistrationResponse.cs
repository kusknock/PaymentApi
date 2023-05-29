using Microsoft.AspNetCore.Identity;
using PaymentApi.Models;
using PaymentApi.Models.IdentityModels;
using System.Collections.Generic;

namespace PaymentApi.Models
{
    /// <summary>
    /// ����� ����� �����������
    /// </summary>
    public class RegistrationResponse : IIdentityResponse
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
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<IdentityError> Errors { get; private set; }

        /// <summary>
        /// ����������� ��������
        /// </summary>
        /// <param name="user">������ ������������</param>
        public RegistrationResponse(User user)
        {
            Id = user.Id;
            Username = user.UserName;
            Errors = null;
        }
        /// <summary>
        /// ����������� � ��������
        /// </summary>
        /// <param name="model">������ ��� �����������</param>
        /// <param name="errors">������ ������</param>
        public RegistrationResponse(RegistrationRequest model, IEnumerable<IdentityError> errors)
        {
            Id = "-1";
            Username = model.UserName;
            Errors = errors;
        }
    }
}