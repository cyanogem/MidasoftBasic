using BAL.Dominio;
using BAL.DTO;
using System.Net.Http.Headers;
using System.Text;

namespace MidaSoftBasic
{
    public class BasicAuthHandler
    {
        public readonly RequestDelegate _next;
        public readonly string relm;
        private UsuarioBAL bussinessAccessLogic = new UsuarioBAL();

        public BasicAuthHandler(RequestDelegate next, string relm)
        {
            _next = next;
            this.relm = relm;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            try
            {
                //configuracion de autenticacion
                var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];

                UsuarioDTO usuario = new UsuarioDTO();
                usuario = bussinessAccessLogic.ValidarUsuarios(username);

                if (username != usuario.Usuario || password != usuario.contrasena)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
                await _next(context);

            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
