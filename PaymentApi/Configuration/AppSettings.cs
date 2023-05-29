using Microsoft.Extensions.Configuration;

namespace PaymentApi.Configuration
{
    /// <summary>
    /// ������ �������� ����������
    /// <br/>���� ��������� ����, ���� �������� application.json
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// ��������� ����, ������� ������������ ��� �������� JWT ��� ��������������� �������
        /// </summary>
        public string Secret { get; set; }
    }
}