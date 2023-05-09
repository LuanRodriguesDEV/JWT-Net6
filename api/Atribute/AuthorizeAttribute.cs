using api.Exeption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace api.Atribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private const string ACESSO_NAO_PERMITIDO = "Acesso negado.";



        public AuthorizeAttribute()
        {

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous) return;

            var user = context.HttpContext.Items["Id"]!;

            if (user == null) throw new AplicationRequestExeption(ACESSO_NAO_PERMITIDO, HttpStatusCode.Unauthorized);

        }


    }
}
