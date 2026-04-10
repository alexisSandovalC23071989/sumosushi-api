using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Categoria;
using SumoSushiApp_BackEnd.DTOs.Direccion;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Repositories.Utils;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class CategoriasRepositoty : ICategoriaRepository
    {
        private readonly IDBContext _dbContext;

        public CategoriasRepositoty(IDBContext dbContext)
        {
            _dbContext = dbContext;

        }

        //Obtenemos todas las categorias
        public async Task<IEnumerable<CategoriaDTO>> ObtenerCategorias()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<CategoriaDTO>(
                "ObtenerCategorias",
                commandType: CommandType.StoredProcedure);
        }

        ///Obtener Categorias Activas 
        public async Task<IEnumerable<CategoriaDTO>> ObtenerCategoriasActivas()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<CategoriaDTO>(
                "ObtenerCategoriasActivas",
                commandType: CommandType.StoredProcedure);
        }
        ///Obtener Categorias Inactivas
        public async Task<IEnumerable<CategoriaDTO>> ObtenerCategoriasInactivas()
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<CategoriaDTO>(
                "ObtenerCategoriasInactivas",
                commandType: CommandType.StoredProcedure);
        }



        //Metodo para crear la categoria///
        public async Task<(int CodigoError, string Mensaje)> AgregarCategoria(CategoriaCrearDTO dto)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = RepoUtils.MapearParametros(dto, true);
            
            await conexion.ExecuteAsync(
                "RegistrarCategoria", // nombre del procedimiento almacenado
                parametros,
                commandType: CommandType.StoredProcedure);
           
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public Task<RespuestaModel> CrearCategoria(CategoriaCrearDTO dto)
        {
            throw new NotImplementedException();
        }



        ///  Deshabilitar Categorias.//
        public async Task<(int CodigoError, string Mensaje)> DeshabilitarCategoria(int idCategoria)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = new DynamicParameters();

            parametros.Add("p_IdCategoria", idCategoria, DbType.Int32);
            parametros.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("p_Mensaje", dbType: DbType.String, size: 400, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync(
                "DeshabilitarCategoria",
                parametros,
                commandType: CommandType.StoredProcedure);

            int codigo = parametros.Get<int>("p_CodigoError");
            string mensaje = parametros.Get<string>("p_Mensaje")!;
            return (codigo, mensaje);
        }

        //Habilitar Categoria 

        public async Task<(int CodigoError, string Mensaje)> HabilitarCategoria(int idCategoria)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = new DynamicParameters();
            parametros.Add("p_IdCategoria", idCategoria, DbType.Int32);
            parametros.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("p_Mensaje", dbType: DbType.String, size: 400, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("HabilitarCategoria", parametros, commandType: CommandType.StoredProcedure);

            int codigo = parametros.Get<int>("p_CodigoError");
            string mensaje = parametros.Get<string>("p_Mensaje")!;
            return (codigo, mensaje);
        }





        // Actaulizar Categoria 

        public async Task<(int CodigoError, string Mensaje)> ActualizarCategoria(CategoriaActualizarDTO dto)
        {
            using var conexion = _dbContext.MySqlConexion();
            var parametros = new DynamicParameters();

            parametros.Add("p_IdCategoria", dto.IdCategoria, DbType.Int32);
            parametros.Add("p_NuevaDescripcion", dto.NuevaDescripcion, DbType.String);
            parametros.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("p_Mensaje", dbType: DbType.String, size: 400, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("ActualizarCategoria", parametros, commandType: CommandType.StoredProcedure);

            int codigo = parametros.Get<int>("p_CodigoError");
            string mensaje = parametros.Get<string>("p_Mensaje")!;
            return (codigo, mensaje);
        }




    }


}
