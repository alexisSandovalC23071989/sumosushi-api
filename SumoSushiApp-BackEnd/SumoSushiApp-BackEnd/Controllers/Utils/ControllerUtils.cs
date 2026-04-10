using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SumoSushiApp_BackEnd.Controllers.Utils
{
    public class ControllerUtils
    {
        private readonly string _secretKey;
        private readonly TokenValidationParameters _parametrosTokenVencido;
        

        public ControllerUtils(IConfiguration config)
        {
            _secretKey = config["JWT:LlaveToken"]!;
            _parametrosTokenVencido = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };
    }
        public static long ObtenerUsuarioId(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated != true) 
                return 0;

            var idClaim = context.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");
            if (idClaim == null || !long.TryParse(idClaim.Value, out long idUsuario))
                return 0;

            return idUsuario;
        }

        public long ObtenerUsuarioIdTokenVencido(HttpContext context)
        {
            // Obtiene el valor del encabezado "Authorization"
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            // Verifica que el header esté presente y empiece con "Bearer "
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                return 0;

            // Extrae solo el token, quitando el prefijo "Bearer "
            var tokenStr = authHeader.Substring("Bearer ".Length).Trim();

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Valida la firma del token, aunque esté expirado
                var principal = tokenHandler.ValidateToken(tokenStr, _parametrosTokenVencido, out _);

                // Busca el claim con el Id del usuario
                var idClaim = principal.Claims.FirstOrDefault(c => c.Type == "IdUsuario");

                // Intenta convertir el valor del claim a long
                if (idClaim != null && long.TryParse(idClaim.Value, out long idUsuario))
                    return idUsuario;
            }
            catch
            {
                // Si la firma no es válida o hay cualquier error, devuelve 0
                return 0;
            }

            return 0;
        }

    }
}