using Microsoft.AspNetCore.Http;
using PaymentApi.DbLogger;
using PaymentApi.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.Middlewares
{
    /// <summary>
    /// Промежуточная обработка запроса для логирования
    /// </summary>
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="next"><inheritdoc/></param>
        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Метод для вызова Middleware во время запроса
        /// </summary>
        /// <param name="context">Контекст запроса</param>
        /// <param name="appContext">Контекст БД</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ApplicationContext appContext)
        {
            var entityLog = await LogBeforeInvoke(context, new LogRepository(appContext));

            Stream originalBody = context.Response.Body;
            string responseBody = string.Empty;

            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await _next(context);

                    memStream.Position = 0;
                    responseBody = await new StreamReader(memStream).ReadToEndAsync();

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }

            LogAfterInvoke(context, new LogRepository(appContext), responseBody, entityLog.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private static async Task<Log> LogBeforeInvoke(HttpContext context, LogRepository log)
        {
            string ipAddress = context.Connection.RemoteIpAddress.ToString(); // IP адрес клиента
            string port = context.Connection.RemotePort.ToString();
            string endPointPath = context.Request.Path.Value; // эндпоинт куда клиент обращается
            string userAgent = context.Request.Headers["User-Agent"];
            string method = context.Request.Method;

            string queryString = context.Request.QueryString.Value; // get параметры

            string bodyRequest = string.Empty;
            if (context.Request.Method == HttpMethods.Post && context.Request.ContentLength > 0)
            {
                context.Request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
                await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                bodyRequest = Encoding.UTF8.GetString(buffer);
                context.Request.Body.Position = 0;
            }

            StringBuilder formBodyBuilder = new StringBuilder(); // post параметры через форму
            if (context.Request.HasFormContentType)
            {
                foreach (var key in context.Request.Form.Keys)
                    formBodyBuilder.Append($"{key}:{context.Request.Form[key]}, ");
            }

            StringBuilder routeValuesBuilder = new StringBuilder(); // post параметры через словарь значений маршрута
            if (context.Request.RouteValues.Count > 0)
            {
                foreach (var key in context.Request.RouteValues.Keys)
                    routeValuesBuilder.Append($"{key}:{context.Request.RouteValues[key]}, ");
            }

            // значение переменных со списками параметров могут быть пустыми в зависимости типа запроса и способа передачи параметров
            return log.Log(new Log
            {
                CategoryName = "RequestLog",
                LogLevel = "RequestInformation",
                User = "middleware",
                Timestamp = DateTime.Now,
                Msg = string.Format("RemoteIp: {0}:{1};EndPointPath: {2} {3};BodyRequest: {4};QueryString: {5};FormBody: {6}; RouteValues:{7}; UserAgent:{8};",
                    ipAddress, port, method, endPointPath, bodyRequest, queryString, formBodyBuilder, routeValuesBuilder, userAgent)
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="log"></param>
        /// <param name="bodyResponse"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        private static void LogAfterInvoke(HttpContext context, LogRepository log, string bodyResponse, int entityId)
        {
            string ipAddress = context.Connection.RemoteIpAddress.ToString(); // IP адрес клиента
            string port = context.Connection.RemotePort.ToString();
            string statusCode = context.Response.StatusCode.ToString(); // 
            string userAgentRequest = context.Request.Headers["User-Agent"];

            log.Log(new Log
            {
                CategoryName = "ResponseLog",
                LogLevel = $"Information about response of request (logId: {entityId})",
                User = "middleware",
                Timestamp = DateTime.Now,
                Msg = string.Format("RemoteIp: {0}:{1};StatusCode: {2};UserAgent:{3};BodyResponse:{4};",
                    ipAddress, port, statusCode, userAgentRequest, bodyResponse)
            });
        }
    }
}
