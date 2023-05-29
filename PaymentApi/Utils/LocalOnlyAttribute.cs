using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PaymentApi.Models;
using System;
using System.Linq;

namespace PaymentApi.Utils
{
    /// <summary>
    /// �������, ������� ��������� �������� ��� � ������������� IP �������� ������ ��� ����������
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LocalOnlyAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="context">�������� �������</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // �������� ip, ���� �� ���� � ����� ������
            var host = context.HttpContext.Connection.RemoteIpAddress.ToString();

            // ��������� "��������" [�������]
            if (host != "localhost" || host != "80.255.129.49" || host != "::1")
                return; // ���� ����������� ������ �������� ������

            context.Result = new UnauthorizedResult(); // �� ����
        }
    }
}