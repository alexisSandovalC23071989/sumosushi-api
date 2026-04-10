using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.DTOs.Usuario;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;
using System.Reflection;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDBContext _dbContext;
        public UsuarioRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // METODO POST, MAPEA Y TAMBIEN USA OBTENERRESULTADOS POR QUE RECIBE CODIGO ERROR Y MENSAJE
        public async Task<(int CodigoError, string Mensaje)> RegistrarUsuario(AuthRegistroDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();

            // Crear parámetros y mapear con prefijo "p_", el tru indica que recibe codigo y mensaje para procedimientos de registro o update
            var parametros = RepoUtils.MapearParametros(model, true);

            // Consulta a BD
            await conexion.ExecuteAsync("RegistrarUsuario", parametros, commandType: CommandType.StoredProcedure);

            //Obtenermos resultados de la consulta, solo necesario con procedimientos de registro o update
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<AuthValidacionDTO?> ObtenerDatosInicioSesion(string correo)
        {
            using var conexion = _dbContext.MySqlConexion();

            // Consulta a BD, Dapper mapea el resultado a un modelo que se le indique como se ve en RegistrarUsuario()
            // En este caso como solo es un dato (string correo) y no un modelo, se mapea el p_ de forma manual
            return await conexion.QueryFirstOrDefaultAsync<AuthValidacionDTO?>(
                "ObtenerDatosInicioSesion",
                new { p_Correo = correo },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<UsuarioPerfilCompletoDTO?> ObtenerPerfilUsuario(long idUsuario)
        {
            using var conexion = _dbContext.MySqlConexion();
            // Consulta a BD, Dapper mapea el resultado a un modelo que se le indique como se ve en RegistrarUsuario()
            return await conexion.QueryFirstOrDefaultAsync<UsuarioPerfilCompletoDTO>(
                "ObtenerPerfilUsuario",
                new { p_IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<string?> ObtenerRolUsuario(long idUsuario)
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryFirstOrDefaultAsync<string>(
                "ObtenerRolUsuario",
                new { p_IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure);
        }

        /// Actualiza nombre, apellidos y teléfono del usuario.
        public async Task<(int CodigoError, string Mensaje)> ActualizarPerfilUsuario(UsuarioPerfilActualizarDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();

            var parametros = RepoUtils.MapearParametros(model, true);

            await conexion.ExecuteAsync(
                "ActualizarPerfilUsuario", 
                parametros, 
                commandType: CommandType.StoredProcedure);

            return RepoUtils.ObtenerRsultado(parametros);
        }

        /// Desactiva la cuenta del usuario (cambia estado a 'Inactivo').
        public async Task<(int CodigoError, string Mensaje)> DesactivarCuentaUsuario(long idUsuario)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = new DynamicParameters();
            parametros.Add("p_IdUsuario", idUsuario, DbType.UInt64);
            parametros.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("p_Mensaje", dbType: DbType.String, size: 400, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync(
                "DesactivarCuentaUsuario",
                parametros,
                commandType: CommandType.StoredProcedure);

            return (
                parametros.Get<int>("p_CodigoError"),
                parametros.Get<string>("p_Mensaje")!);
        }

        public async Task<InfoUsuarioDTO?> ObtenerUsuarioPorCorreo(ObtenerUsuarioPorCorreoDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, false);
            return await conexion.QueryFirstOrDefaultAsync<InfoUsuarioDTO>(
                "ObtenerUsuarioPorCorreo",
                parametros,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<(int codigoError, string mensaje)> ActualizarRolUsuario(ActualizarRolDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();

            var parametros = RepoUtils.MapearParametros(model, true);

            // Consulta a BD
            await conexion.ExecuteAsync("ActualizarRolUsuario", parametros, commandType: CommandType.StoredProcedure);
            //Obtenermos resultados de la consulta, solo necesario con procedimientos de registro o update
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<(int codigoError, string mensaje)> AdminDesactivarCuenta(AdminGestionCuentasDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync("DeshabilitarUsuarioPorCorreo", parametros, commandType: CommandType.StoredProcedure);

            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<(int codigoError, string mensaje)> AdminReactivarCuenta(AdminGestionCuentasDTO model)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync("RehabilitarUsuarioPorCorreo", parametros, commandType: CommandType.StoredProcedure);

            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<IEnumerable<InfoUsuarioDTO>> ObtenerColaboradoresRestaurante()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<InfoUsuarioDTO>(
                "ObtenerColaboradoresRestaurante",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<InfoUsuarioDTO>> ObtenerUsuariosDeshabilitados()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<InfoUsuarioDTO>(
                "ObtenerUsuariosDeshabilitados",
                commandType: CommandType.StoredProcedure);
        }        
    }
}