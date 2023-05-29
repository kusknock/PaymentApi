using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PaymentApi.Models;
using System;
using System.Linq;

namespace PaymentApi.Utils
{
    /// <summary>
    /// ������� ������ ��� ��������������� ������� � ������� ��� ������������
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="context">�������� �������</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // �������� �������� ������������ �� ���������
            var user = context.HttpContext.Items["User"] as User;
            
            // �������� ip, ���� �� ���� � ����� ������
            var ipAddress = context.HttpContext.Items["IpAddress"] as IpAddress;

            // ��������� "��������" [�������]
            if (user != null || ipAddress != null)
                return; // ���� ����������� ������ �������� ������

            context.Result = new UnauthorizedResult(); // �� ����
        }
    }
}