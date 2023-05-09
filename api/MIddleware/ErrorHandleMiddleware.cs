using api.Exeption;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net;

namespace BackEnd_Clinica.MIddleware
{
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandleMiddleware> _logger;

        public ErrorHandleMiddleware(RequestDelegate next, ILogger<ErrorHandleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AplicationRequestExeption ex)
            {
                await HandleValidateException(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandleMiddleware> logger)
        {

            string id_error = Guid.NewGuid().ToString();

            logger.LogError($"Mensagem erro {id_error}: {exception.Message}");
            logger.LogError($"StackTrace {id_error}: {exception.ToString()}");

            var code = HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new ErroInterno()
            {
                Id = id_error
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleValidateException(HttpContext context, AplicationRequestExeption exception)
        {
            var code = exception.StatusCode;
            var result = JsonConvert.SerializeObject(exception.Resposta);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }


    public class ErroInterno
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "mensagem")]
        public string? Mensagem { get; set; } = "Erro inesperado no Servidor.";
    }
}
