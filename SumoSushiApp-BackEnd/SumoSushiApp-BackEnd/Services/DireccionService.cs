using SumoSushiApp_BackEnd.DTOs.Direccion;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class DireccionService : IDireccionService
    {
        private readonly IDireccionRepository _direccionRepository;

        public DireccionService(IDireccionRepository direccionRepository)
        {
            _direccionRepository = direccionRepository;
        }

        public async Task<RespuestaModel> ObtenerDireccionesUsuario(long idUsuario)
        {
            var lista = await _direccionRepository.ObtenerDireccionesUsuario(idUsuario);
            return RespuestasUtils.GenerarRespuesta(lista);
        }

        public async Task<RespuestaModel> AgregarDireccion(long idUsuario, DireccionCrearDTO dto)
        {
            var (codigo, mensaje) = await _direccionRepository.AgregarDireccionUsuario(idUsuario, dto);
            return RespuestasUtils.GenerarRespuesta(codigo, mensaje);
        }

        public async Task<RespuestaModel> ActualizarDireccion(long idUsuario, DireccionActualizarDTO dto)
        {
            var (codigo, mensaje) = await _direccionRepository.ActualizarDireccionUsuario(idUsuario, dto);
            return RespuestasUtils.GenerarRespuesta(codigo, mensaje);
        }

        public async Task<RespuestaModel> DeshabilitarDireccion(long idUsuario, long idDireccion)
        {
            var (codigo, mensaje) = await _direccionRepository.DeshabilitarDireccionUsuario(idUsuario, idDireccion);
            return RespuestasUtils.GenerarRespuesta(codigo, mensaje);
        }

        public async Task<RespuestaModel> ObtenerCantones()
        {
            var lista = await _direccionRepository.ObtenerCantones();
            return RespuestasUtils.GenerarRespuesta(lista);
        }

        public async Task<RespuestaModel> ObtenerDistritos()
        {
            var lista = await _direccionRepository.ObtenerDistritosPorCanton();
            return RespuestasUtils.GenerarRespuesta(lista);
        }
    }
}