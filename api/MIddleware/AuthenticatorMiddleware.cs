using api.Exeption;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace api.MIddleware
{
    public class AuthenticatorMiddleware
    {
        private readonly RequestDelegate _next;


        private const string ACESSO_NAO_PERMITIDO = "Acesso negado.";

        public AuthenticatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();


            if (token != null) TokenValidate(httpContext, token);

            return _next(httpContext);
        }

        private void TokenValidate(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c7829-ejqoe23-ridsaj-213-dasduqw"));
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidIssuer = "639fac0dc68cb2a1ab0a4c61104fb259753ffb11",
                    ValidAudience = "c3409ca-973685dd-b44e15da-fe0f2bac0-e1dea057",
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);


                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "sub").Value;

                context.Items["Id"] = userId;

            }
            catch (Exception)
            {
                throw new AplicationRequestExeption(ACESSO_NAO_PERMITIDO, HttpStatusCode.Unauthorized);
            }
        }
    }
}
