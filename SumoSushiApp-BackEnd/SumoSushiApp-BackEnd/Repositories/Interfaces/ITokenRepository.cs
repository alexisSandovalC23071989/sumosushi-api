using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.DTOs.Token;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        public Task<(int, string)> RegistrarRefreshToken(AuthNuevoRefreshTokenDTO model);
        public Task<AuthRefreshTokenInfoDTO?> UtilizarRefreshToken(string refreshToken);
        public Task<(int, string)> RegistrarTokenRecuperacion(AuthTokenRecuperacionDTO model);
        public Task<AuthTokenRecuperacionDTO?> UtilizarTokenRecuperacion(AuthRecuperacionContrasennaDTO model);
        public Task<(int, string)> RevocarRefreshToken(CierreSesionDTO model);
    }
}
