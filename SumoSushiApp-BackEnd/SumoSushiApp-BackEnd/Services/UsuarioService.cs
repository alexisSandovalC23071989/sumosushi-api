using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.DTOs.Direccion;
using SumoSushiApp_BackEnd.DTOs.Usuario;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class UsuarioService : IUsuarioService
    {
        //Inyeccion de dependencias        
        // Repositorios
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IDireccionRepository _direccionRepository;
        private readonly ITokenRepository _tokenRepository;

        // Servicios auxiliares
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;


        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IHashService hashService,
            ITokenService jwtService,
            ITelefonoRepository telefonoRepository,
            IDireccionRepository direccionRepository,
            ITokenRepository tokenRepository)
        {
            _usuarioRepository = usuarioRepository;
            _hashService = hashService;
            _tokenService = jwtService;
            _telefonoRepository = telefonoRepository;
            _direccionRepository = direccionRepository;
            _tokenRepository = tokenRepository;
        }

        #region Usuarios
        public async Task<RespuestaModel> RegistrarUsuario(AuthRegistroDTO model)
        {
              
            model.Contrasenna = _hashService.HashContrasenna(model.Contrasenna!);

            // POST 
            var (codigoError, mensaje) = await _usuarioRepository.RegistrarUsuario(model);

            return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
        }

        /// <summary>
        /// Al iniciar sesion, se devuelve un RespustaModel y un RefreshToken, ya que el RefreshToken 
        /// se envia al cliente en el encabezado como una cookie segura, para guardar en HTTPOnly. 
        /// </summary>
        public async Task<(RespuestaModel, string?)> InicioSesion(AuthLoginDTO model)
        {
            // GET IdUsuario, Contrasenna y Rol
            var datosUsuario = await _usuarioRepository.ObtenerDatosInicioSesion(model.Correo!);
            // Validacion de contraseña
            if (datosUsuario == null || !_hashService.VerificarContrasenna(model.Contrasenna!, datosUsuario.Contrasenna!))            
                    return (RespuestasUtils.GenerarRespuesta(1, "Correo o contraseña incorrectos"), null);

            // Generar Refresh Token y guardarlo en la base de datos
            var refreshTokenInfo = new AuthNuevoRefreshTokenDTO();
            var codigoError = 0;
            var mensaje = string.Empty;
            do
            {
                refreshTokenInfo = _tokenService.GenerarRefreshToken(datosUsuario);
                (codigoError, mensaje) = await _tokenRepository.RegistrarRefreshToken(refreshTokenInfo);
            } while (codigoError == 1);

            if (codigoError != 0)
                return (RespuestasUtils.GenerarRespuesta(codigoError, mensaje), null);

            var accestoken = new AuthNuevoAccessTokenDTO {AccesToken = _tokenService.GenerarAccessToken(datosUsuario)};

            return (RespuestasUtils.GenerarRespuesta(accestoken), refreshTokenInfo.RefreshToken);
        }

        public async Task<(RespuestaModel?, string?)> RefreshAccesToken(long idUsuario, string refreshToken)
        {
            var refreshTokenInfo = await _tokenRepository.UtilizarRefreshToken(refreshToken);

            // Validaciones
            if (refreshTokenInfo == null || refreshTokenInfo.Revocado)
                return (RespuestasUtils.GenerarRespuestaAlerta("Por favor, iniciar sesion"), "");
            if (refreshTokenInfo.Utilizado > 1 || refreshTokenInfo.IdUsuario != idUsuario)
                return (RespuestasUtils.GenerarRespuestaAlerta("Se detecto una vulnerabilidad, vuelva a iniciar sesión"), "");
            if (refreshTokenInfo.FechaCaducidad < DateTime.UtcNow)
                return (RespuestasUtils.GenerarRespuestaAlerta("La sesión a expirado, por favor vuelva a iniciar sesión"), "");

            var datosUsuario = new AuthValidacionDTO
            {
                IdUsuario = idUsuario,
                Rol = refreshTokenInfo.Rol
            };
            // Generamos un nuevo refresh token y lo guardamos en la base de datos
            var nuevoRefreshToken = _tokenService.GenerarRefreshToken(datosUsuario);

            (var codigoError, var mensaje) = await _tokenRepository.RegistrarRefreshToken(nuevoRefreshToken);
            if (codigoError != 0)
                return (RespuestasUtils.GenerarRespuesta(codigoError, mensaje), null);

            var tokens = new AuthNuevoAccessTokenDTO();
            tokens.AccesToken = _tokenService.GenerarAccessToken(datosUsuario);

            // nuevoRefreshToken.RefreshToken
            return (RespuestasUtils.GenerarRespuesta(tokens), nuevoRefreshToken.RefreshToken);
        }

        /// <summary>
        /// Por ser una consulta complicada, donde se devuelve un juego de datos junto a lista de telefonos y direcciones,
        /// lo mejor es dividir la consulta en 3 partes, una para el perfil del usuario, otra para los telefonos y otra para las direcciones.
        /// De esta forma no traemos datos repetidos y evitamos problemas de rendimiento.
        /// </summary>
        public async Task<RespuestaModel> ObtenerPerfilUsuario(long idUsuario)
        {
            var perfilUsuario = await _usuarioRepository.ObtenerPerfilUsuario(idUsuario);
            if (perfilUsuario == null)
                return RespuestasUtils.GenerarRespuesta(1, "No se encontró el perfil del usuario");

            // Iniciar las tareas en paralelo
            var telefonosTask = _telefonoRepository.ObtenerTelefonosUsuario(idUsuario);
            var direccionesTask = _direccionRepository.ObtenerDireccionesUsuario(idUsuario);

            // Esperar ambas tareas que corren en paralelo
            var telefonos = await telefonosTask;
            var direcciones = await direccionesTask;

            perfilUsuario.Telefonos = telefonos!;
            perfilUsuario.Direcciones = (IEnumerable<DireccionCompletaDTO>)direcciones;

            return RespuestasUtils.GenerarRespuesta(perfilUsuario!);
        }

        /// Actualiza los datos de perfil del usuario.
        public async Task<RespuestaModel> ActualizarPerfilUsuario(UsuarioPerfilActualizarDTO model)
        {
            var (codigo, mensaje) = await _usuarioRepository.ActualizarPerfilUsuario(model);
            return RespuestasUtils.GenerarRespuesta(codigo, mensaje);
        }

        /// Desactiva la cuenta del usuario y devuelve confirmación.
        public async Task<RespuestaModel> DesactivarCuenta(long idUsuario)
        {
            var (codigo, mensaje) = await _usuarioRepository.DesactivarCuentaUsuario(idUsuario);
            return RespuestasUtils.GenerarRespuesta(codigo, mensaje);
        }        
        #endregion

        //============================================================================================

        #region Administrador
        public async Task<RespuestaModel> ActualizarRolUsuario(ActualizarRolDTO model)
        {
            var (codigoError, mensaje) = await _usuarioRepository.ActualizarRolUsuario(model);
            return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
        }

        public async Task<RespuestaModel> AdminDesactivarCuenta(AdminGestionCuentasDTO model)
        {
            var (codigoError, mensaje) = await _usuarioRepository.AdminDesactivarCuenta(model);
            return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
        }

        public async Task<RespuestaModel> AdminReactivarCuenta(AdminGestionCuentasDTO model)
        {
            var (codigoError, mensaje) = await _usuarioRepository.AdminReactivarCuenta(model);
            return RespuestasUtils.GenerarRespuesta(codigoError, mensaje);
        }

        public async Task<RespuestaModel> ObtenerColaboradoresRestaurante()
        {
            var colaboradores = await _usuarioRepository.ObtenerColaboradoresRestaurante();
            return RespuestasUtils.GenerarRespuesta(colaboradores);
        }

        public async Task<RespuestaModel> ObtenerUsuarioPorCorreo(ObtenerUsuarioPorCorreoDTO model)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorCorreo(model);
            if (usuario == null)
                return RespuestasUtils.GenerarRespuesta(1, "No se encontró el usuario con el correo proporcionado.");
            return RespuestasUtils.GenerarRespuesta(usuario);
        }

        public async Task<RespuestaModel> ObtenerUsuariosDeshabilitados()
        {
            var usuarios = await _usuarioRepository.ObtenerUsuariosDeshabilitados();
            if (usuarios == null)
                return RespuestasUtils.GenerarRespuesta(1, "No hay usuarios deshabilitados");
            return RespuestasUtils.GenerarRespuesta(usuarios);
        }
        #endregion

    }
}