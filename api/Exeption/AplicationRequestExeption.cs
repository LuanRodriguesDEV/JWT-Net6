using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net;

namespace api.Exeption
{
    public class AplicationRequestExeption : Exception
    {
        public Retorno Resposta { get; }
        public HttpStatusCode StatusCode { get; }

        public AplicationRequestExeption(string message, ModelStateDictionary modelState) : base(message)
        {
            Resposta = new Retorno()
            {
                Id = Guid.NewGuid().ToString(),
                Messagem = message,
                Campos = new List<CamposErro>()
            };

            foreach (var campo in modelState.Keys)
            {
                foreach (var erro in modelState.GetValueOrDefault(campo)!.Errors)
                {
                    Resposta.Campos.Add(new CamposErro()
                    {
                        Campo = campo,
                        Mensagem = erro.ErrorMessage,
                    });
                }
            }

            StatusCode = HttpStatusCode.BadRequest;
        }

        public AplicationRequestExeption(string message, HttpStatusCode statusCode) : base(message)
        {
            Resposta = new Retorno()
            {
                Id = Guid.NewGuid().ToString(),
                Messagem = message,
                Campos = null
            };

            StatusCode = statusCode;
        }
    }
    public class Retorno
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "mensagem")]
        public string? Messagem { get; set; }

        [JsonProperty("campos", NullValueHandling = NullValueHandling.Ignore)]
        public List<CamposErro>? Campos { get; set; } = new List<CamposErro>();
    }

    public class CamposErro
    {
        [JsonProperty(PropertyName = "campo")]
        public string? Campo { get; set; }

        [JsonProperty(PropertyName = "mensagem")]
        public string? Mensagem { get; set; }
    }
}
