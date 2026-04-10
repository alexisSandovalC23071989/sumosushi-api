
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Menu;
using System.Data;

﻿using Dapper;
using SumoSushiApp_BackEnd.DTOs.Producto;
using SumoSushiApp_BackEnd.Repositories.Utils;
using SumoSushiApp_BackEnd.DTOs.Etiqueta;


namespace SumoSushiApp_BackEnd.Repositories
{
    public class ProductoRepository : IProductoRepository
    {

        private readonly DBContext _dbContext;

        public ProductoRepository(DBContext context)
        {
            _dbContext = context;
        }
        #region Stheven
        public List<ProductoMenuDTO> ObtenerMenuCompleto()
        {
            var productos = new List<ProductoMenuDTO>();

            using var conexion = _dbContext.MySqlConexion();
            conexion.Open();

            using var cmd = conexion.CreateCommand();
            cmd.CommandText = "ObtenerMenuCompleto";
            cmd.CommandType = CommandType.StoredProcedure;

            var p_CodigoError = cmd.CreateParameter();
            p_CodigoError.ParameterName = "p_CodigoError";
            p_CodigoError.Direction = ParameterDirection.Output;
            p_CodigoError.DbType = DbType.Int32;

            var p_Mensaje = cmd.CreateParameter();
            p_Mensaje.ParameterName = "p_Mensaje";
            p_Mensaje.Direction = ParameterDirection.Output;
            p_Mensaje.DbType = DbType.String;

            cmd.Parameters.Add(p_CodigoError);
            cmd.Parameters.Add(p_Mensaje);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                productos.Add(new ProductoMenuDTO
                {
                    IdProducto = reader.GetInt32(reader.GetOrdinal("p_IdProducto")),
                    Nombre = reader.GetString(reader.GetOrdinal("p_Nombre")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("p_Descripcion")) ? null : reader.GetString(reader.GetOrdinal("p_Descripcion")),
                    ImagenUrl = reader.IsDBNull(reader.GetOrdinal("p_ImagenUrl")) ? null : reader.GetString(reader.GetOrdinal("p_ImagenUrl")),
                    Categoria = reader.IsDBNull(reader.GetOrdinal("p_Categoria")) ? null : reader.GetString(reader.GetOrdinal("p_Categoria")),
                    Etiquetas = reader.IsDBNull(reader.GetOrdinal("p_Etiquetas")) ? new List<string>() : reader.GetString(reader.GetOrdinal("p_Etiquetas")).Split(", ").ToList(),
                    EsPromocion = reader.GetBoolean(reader.GetOrdinal("p_EsPromocion"))
                });
            }

            return productos;
        }

        public ProductoMenuDTO? ObtenerDetalleProducto(int id)
        {
            using var conexion = _dbContext.MySqlConexion();
            conexion.Open();

            using var cmd = conexion.CreateCommand();
            cmd.CommandText = "ObtenerDetalleProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            var paramId = cmd.CreateParameter();
            paramId.ParameterName = "p_IdProducto";
            paramId.Value = id;
            cmd.Parameters.Add(paramId);

            var p_CodigoError = cmd.CreateParameter();
            p_CodigoError.ParameterName = "p_CodigoError";
            p_CodigoError.Direction = ParameterDirection.Output;
            p_CodigoError.DbType = DbType.Int32;
            cmd.Parameters.Add(p_CodigoError);

            var p_Mensaje = cmd.CreateParameter();
            p_Mensaje.ParameterName = "p_Mensaje";
            p_Mensaje.Direction = ParameterDirection.Output;
            p_Mensaje.DbType = DbType.String;
            cmd.Parameters.Add(p_Mensaje);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new ProductoMenuDTO
                {
                    IdProducto = reader.GetInt32(reader.GetOrdinal("p_IdProducto")),
                    Nombre = reader.GetString(reader.GetOrdinal("p_Nombre")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("p_Descripcion")) ? null : reader.GetString(reader.GetOrdinal("p_Descripcion")),
                    ImagenUrl = reader.IsDBNull(reader.GetOrdinal("p_ImagenUrl")) ? null : reader.GetString(reader.GetOrdinal("p_ImagenUrl")),
                    Categoria = reader.IsDBNull(reader.GetOrdinal("p_Categoria")) ? null : reader.GetString(reader.GetOrdinal("p_Categoria")),
                    Etiquetas = reader.IsDBNull(reader.GetOrdinal("p_Etiquetas")) ? new List<string>() : reader.GetString(reader.GetOrdinal("p_Etiquetas")).Split(", ").ToList(),
                    EsPromocion = reader.GetBoolean(reader.GetOrdinal("p_EsPromocion"))
                };
            }

            return null;
        }

        public (string mensaje, int codigoError) CalificarProducto(long usuarioId, int productoId, int calificacion)
        {
            using var conexion = _dbContext.MySqlConexion();
            conexion.Open();

            using var cmd = conexion.CreateCommand();
            cmd.CommandText = "CalificarProducto";
            cmd.CommandType = CommandType.StoredProcedure;

            var paramUsuario = cmd.CreateParameter();
            paramUsuario.ParameterName = "p_IdUsuario";
            paramUsuario.Value = usuarioId;
            cmd.Parameters.Add(paramUsuario);

            var paramProducto = cmd.CreateParameter();
            paramProducto.ParameterName = "p_IdProducto";
            paramProducto.Value = productoId;
            cmd.Parameters.Add(paramProducto);

            var paramCalificacion = cmd.CreateParameter();
            paramCalificacion.ParameterName = "p_Calificacion";
            paramCalificacion.Value = calificacion;
            cmd.Parameters.Add(paramCalificacion);

            var p_CodigoError = cmd.CreateParameter();
            p_CodigoError.ParameterName = "p_CodigoError";
            p_CodigoError.Direction = ParameterDirection.Output;
            p_CodigoError.DbType = DbType.Int32;
            cmd.Parameters.Add(p_CodigoError);

            var p_Mensaje = cmd.CreateParameter();
            p_Mensaje.ParameterName = "p_Mensaje";
            p_Mensaje.Direction = ParameterDirection.Output;
            p_Mensaje.Size = 500; // Ajuste recomendado
            p_Mensaje.DbType = DbType.String;
            cmd.Parameters.Add(p_Mensaje);

            cmd.ExecuteNonQuery();

            int codigoError = Convert.ToInt32(p_CodigoError.Value);
            string mensaje = p_Mensaje.Value?.ToString() ?? "";

            return (mensaje, codigoError);
        }

        public ProductoMenuDTO? ObtenerDetalle(int id)
        {
            throw new NotImplementedException();
        }

        public bool CalificarProducto(int id, int usuarioId, int calificacion, out string mensaje)
        {
            throw new NotImplementedException();
        }

        public Task<(int codigoError, string mensaje)> CalificarProducto(int usuarioId, int productoId, int calificacion)
        {
            throw new NotImplementedException();
        }
        #endregion


        public async Task<(int codigoError, string mensaje, long IdProducto)> RegistrarProducto(
            NuevoProductoDTO model,
            IDbConnection conexion, 
            IDbTransaction transaccion)
        {
            var parametros = RepoUtils.MapearParametros(model, true);
            parametros.Add("p_IdProducto", dbType: DbType.Int64, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync(
                "RegistrarProducto", 
                parametros, 
                transaction: transaccion, 
                commandType: CommandType.StoredProcedure);

            long idProducto = parametros.Get<long>("p_IdProducto");
            var (codigoError, mensaje) = RepoUtils.ObtenerRsultado(parametros);

            return (codigoError, mensaje, idProducto);
        }

        public async Task<(int codigoError, string mensaje)> ActualizarProducto(
            ActualizarDatosProductoDTO model,
            IDbConnection conexion,
            IDbTransaction transaccion)
        {
            var parametros = RepoUtils.MapearParametros(model, true);

            await conexion.ExecuteAsync(
                "ActualizarProducto",
                parametros,
                transaction: transaccion,
                commandType: CommandType.StoredProcedure);
            

            return RepoUtils.ObtenerRsultado(parametros);
        }


        public async Task<(int codigoError, string mensaje)> RegistrarPresentacionProducto(
            RegistroPresentacionProductoDTO model, 
            IDbConnection conexion, 
            IDbTransaction transaccion)
        {
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync(
                "RegistrarPresentacionesProducto", 
                parametros, 
                transaction: transaccion, 
                commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<(int codigoError, string mensaje)> ActualizarPresentacionProducto(
            ActualizarPresentacionProductoDTO model,
            IDbConnection conexion,
            IDbTransaction transaccion)
        {
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync(
                "ActualizarPresentacionProducto",
                parametros,
                transaction: transaccion,
                commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }

        public async Task<(int codigoError, string mensaje)> RegistraEtiquetaProducto(
            EtiquetaProductoDTO model, 
            IDbConnection conexion, 
            IDbTransaction transaccion)
        {
            var parametros = RepoUtils.MapearParametros(model, true);
            await conexion.ExecuteAsync(
                "RegistrarEtiquetasProducto", 
                parametros, 
                transaction: transaccion, 
                commandType: CommandType.StoredProcedure);
            return RepoUtils.ObtenerRsultado(parametros);
        }
    }
}


