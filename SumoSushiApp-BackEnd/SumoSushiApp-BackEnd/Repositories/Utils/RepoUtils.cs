using Dapper;
using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace SumoSushiApp_BackEnd.Repositories.Utils
{
    public static class RepoUtils
    {
        /// Este metodo mapea un objeto con el prefijo "p_" para enviar como parámetros a un procedimiento almacenado.
        /// Solo funciona con objetos, no con datos unicos como string, int, etc.
        /// Se puede utilizar siempre con post = true, pero es mejor usarlo en false cuando no se van a recibir 
        /// codigo y mensaje de respuesta. 
        public static DynamicParameters MapearParametros(object model, bool post = true)
        {
            const string PREFIJO = "p_";
            var parametros = new DynamicParameters();
            if (model == null) return parametros;
            var propiedades = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(prop => !typeof(IFormFile).IsAssignableFrom(prop.PropertyType));
            foreach (var prop in propiedades)
            {
                var nombre = PREFIJO + prop.Name;
                var valor = prop.GetValue(model);
                parametros.Add(nombre, valor);
            }
            // ** En POST y UPDATE necesitamos los parámetros de salida para obtener el código de error y mensaje ** 
            if (post)
            {
                parametros.Add("p_CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("p_Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            }
            return parametros;
        }

        /// Este metodo extrae el código de error y mensaje de los parámetros de salida de un procedimiento almacenado,
        /// solo se utiliza cuando se hace un POST o UPDATE que retorna estos valores.
        public static (int codigoError, string mensaje) ObtenerRsultado(DynamicParameters parametros)
        {
            int codigoError = parametros.Get<int>("p_CodigoError");
            string mensaje = parametros.Get<string>("p_Mensaje");
            return (codigoError, mensaje);
        }
    }
}