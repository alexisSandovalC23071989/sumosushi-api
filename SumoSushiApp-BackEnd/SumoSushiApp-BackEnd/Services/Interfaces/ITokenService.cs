using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerarAccessToken(AuthValidacionDTO model);
        public AuthNuevoRefreshTokenDTO GenerarRefreshToken(AuthValidacionDTO model);
        public AuthTokenRecuperacionDTO GenerarTokenRecuperacion(AuthSolicitudRecuperacionDTO model);
    }
}
