using SumoSushiApp_BackEnd.Models;

namespace SumoSushiApp_BackEnd.Services.Utils
{
    public static class RespuestasUtils
    {
        /// <summary>
        /// Esete metodo genera una respuesta de tipo RespuestaModel lista para enviar al cliente,
        /// gracias a la sobrecarga se puede utilizar para generar respuestas de exito o error,
        /// o para generar una respuesta con datos obtenidos de la base de datos.
        /// 
        /// Vean que ambos metodos se llaman igual pero reciben datos diferentes, 
        /// el compilador sabe cual utilizar automaticament segun los parametros que se le pasen.
        /// </summary>
        public static RespuestaModel GenerarRespuesta(int codigoError, string mensaje)
        {
            return codigoError switch
            {
                0 => new RespuestaModel { Exito = true, Mensaje = mensaje },
                1 => new RespuestaModel { Exito = false, Mensaje = mensaje },
                _ => new RespuestaModel
                {
                    Exito = false,
                    Mensaje = "Error inesperado en la base de datos:" + mensaje
                }
            };
        }

        public static RespuestaModel GenerarRespuesta(object? model)
        {
            var respuesta = new RespuestaModel();
            if (model is null)
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "No se encontraron resultados";
            }
            else
            {
                respuesta.Exito = true;
                respuesta.Datos = model;
            }
            return respuesta;
        }

        // Metodo utilizado para generar respuestas con alertas al frontend
        // por ejemplo, cuando se detecta una vulnerabilidad en refresh token
        public static RespuestaModel GenerarRespuestaAlerta(string mensaje, int alerta = 1)
        {
            var respuesta = new RespuestaModel
            {
                Exito = false,
                Mensaje = mensaje,
                Alerta = alerta
            };

            return respuesta;
        }
    }
}