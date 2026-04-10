using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ICorreoService _correoService;
        private readonly IAuthRepository _authRepository;

        // Servicios auxiliares
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;


        public AuthService(
            IUsuarioRepository usuarioRepository,
            IHashService hashService,
            ITokenService tokenService,
            ITelefonoRepository telefonoRepository,
            IDireccionRepository direccionRepository,
            ITokenRepository tokenRepository,
            ICorreoService correoService,
            IAuthRepository authRepository)
        {
            _usuarioRepository = usuarioRepository;
            _hashService = hashService;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
            _correoService = correoService;
            _authRepository = authRepository;
        }

        public async Task<RespuestaModel> SolicitarRecuperarContrasenna(AuthSolicitudRecuperacionDTO model)
        {
            var codigoError = 0;
            var mensaje = string.Empty;
            var tokenRecuperacion = new AuthTokenRecuperacionDTO();
            do
            {
                tokenRecuperacion = _tokenService.GenerarTokenRecuperacion(model);
                (codigoError, mensaje) = await _tokenRepository.RegistrarTokenRecuperacion(tokenRecuperacion);

            } while (codigoError != 0);

            var correo = await _correoService.GenerarCorreo(tokenRecuperacion);
            var resultadoEnvio = await _correoService.EnviarCorreo(model.Correo!, "Solicitud de recuperación de contraseña", correo, true);

            if (!resultadoEnvio)
                return RespuestasUtils.GenerarRespuesta(1, "Error al enviar el correo de recuperación. Por favor, inténtelo de nuevo más tarde.");

            return RespuestasUtils.GenerarRespuesta(0, "Se ha enviado un token de recuperación al correo " + 
                model.Correo + "\nEl token tiene una validez de 15 minutos");

        }

        public async Task<RespuestaModel> RecuperarContrasennaRecuperacion(AuthRecuperacionContrasennaDTO model)
        {
            var tokenEncontrado = await _tokenRepository.UtilizarTokenRecuperacion(model);

            if (tokenEncontrado == null)
                return RespuestasUtils.GenerarRespuesta(1, "Error en el servidor");
            if (tokenEncontrado != null && tokenEncontrado.Token == null)
                return RespuestasUtils.GenerarRespuesta(1, "Token exporado o ya ha sido utilizado.");
            if (tokenEncontrado!.Correo != model.Correo ||
                tokenEncontrado.Token != model.Token)
                return RespuestasUtils.GenerarRespuesta(1, "El correo suministrado no coincide con el Token");
            if (tokenEncontrado.FechaExpiracion < DateTime.UtcNow)
                return RespuestasUtils.GenerarRespuesta(1, "El token ha expirado, por favor solicite uno nuevo.");

            // Si el token es válido, actualizamos la contraseña del usuario
            model.Contrasenna = _hashService.HashContrasenna(model.Contrasenna!);
            (var codigoError, var mensaje) = await _authRepository.CambiarContrasennaRecuperacion(model);
            return (RespuestasUtils.GenerarRespuesta(codigoError, mensaje));

        }

        public async Task<RespuestaModel> CambiarContrasennaLogueado(AuthContrasennasUsuarioDTO model)
        {
            var datosValidacion = await _authRepository.DatosValidacionCambioContrasenna(model);

            if (datosValidacion == null || 
                !_hashService.VerificarContrasenna(model.Contrasenna!, datosValidacion.Contrasenna!))
                return (RespuestasUtils.GenerarRespuesta(1, "Correo o contraseña incorrectos"));

            datosValidacion.Contrasenna = _hashService.HashContrasenna(model.NuevaContrasenna!);

            (var codigoError, var mensaje) = await _authRepository.CambiarContrasennaRecuperacion(datosValidacion);
            return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
        }

        public async Task<RespuestaModel> CierreSesion(CierreSesionDTO model)
        {
            (var codigoError, var mensaje) = await _tokenRepository.RevocarRefreshToken(model);
            return RespuestasUtils.GenerarRespuesta(0, "Se ha cerrado la sesión");
        }
    }
}
