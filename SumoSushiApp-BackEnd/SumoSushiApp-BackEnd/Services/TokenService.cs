using Microsoft.IdentityModel.Tokens;
using SumoSushiApp_BackEnd.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using SumoSushiApp_BackEnd.DTOs.Auth;

namespace SumoSushiApp_BackEnd.Services
{
    public class TokenService : ITokenService
    {
        // Acces Token
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _tokenExpiration;

        // Refresh Token
        private readonly int _refreshTokenTamanio;
        private readonly int _refreshTokenDuracion;

        private readonly SymmetricSecurityKey _key;
        private readonly SigningCredentials _credentials;

        public TokenService(IConfiguration configuration)
        {
            // AccesToken
            _secretKey = configuration.GetSection("JWT:LlaveToken").Value!;
            _issuer = configuration.GetSection("JWT:Issuer").Value!;
            _audience = configuration.GetSection("JWT:Audience").Value!;
            _tokenExpiration = int.Parse(configuration.GetSection("JWT:TokenExpirationMinutes").Value!);
            if (string.IsNullOrEmpty(_secretKey) || Encoding.UTF8.GetBytes(_secretKey).Length < 32)
                throw new ArgumentException("La clave JWT debe tener al menos 32 caracteres para garantizar seguridad");
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            _credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            // RefreshToken
            _refreshTokenTamanio = int.Parse(configuration.GetSection("RefreshToken:Tamanio").Value!);
            _refreshTokenDuracion = int.Parse(configuration.GetSection("RefreshToken:DiasExpiracion").Value!);

        }

        public string GenerarAccessToken(AuthValidacionDTO model)
        {
            var claims = new List<Claim>
            {
                new Claim("IdUsuario", model.IdUsuario.ToString()),
                new Claim("Rol", model.Rol!)
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenExpiration),
                signingCredentials: _credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthNuevoRefreshTokenDTO GenerarRefreshToken(AuthValidacionDTO model)
        {
            var refreshToken = new byte[_refreshTokenTamanio];
            var fechaInicio = DateTime.UtcNow;
            var fechaCaducidad = fechaInicio.AddDays(_refreshTokenDuracion);

            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(refreshToken);            

            return new AuthNuevoRefreshTokenDTO
            {
                IdUsuario = model.IdUsuario,
                RefreshToken = Convert.ToBase64String(refreshToken),
                FechaInicio = fechaInicio,
                FechaCaducidad = fechaCaducidad
            };
        }

        public AuthTokenRecuperacionDTO GenerarTokenRecuperacion(AuthSolicitudRecuperacionDTO model)
        {
            int longitud = 6;
            int minutosExpiracion = 15;
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var tokenRecuperacion = new AuthTokenRecuperacionDTO
            {
                Correo = model.Correo
            };
            tokenRecuperacion.Token = new string(Enumerable.Range(0, longitud)
                .Select(_ => caracteres[random.Next(caracteres.Length)]).ToArray());
            tokenRecuperacion.FechaExpiracion = DateTime.UtcNow.AddMinutes(minutosExpiracion);

            return tokenRecuperacion;
        }
    }
}
