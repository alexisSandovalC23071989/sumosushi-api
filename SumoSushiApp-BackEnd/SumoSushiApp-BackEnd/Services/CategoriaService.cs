using SumoSushiApp_BackEnd.DTOs.Categoria;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        //TODO: Inyectar repositorio
        public CategoriaService(ICategoriaRepository categoriaRepository) {
            _categoriaRepository = categoriaRepository;
        }
        //Obtener categorias totales
        public async Task<RespuestaModel> ObtenerCategorias()
        {
            var listaDeCategorias = await _categoriaRepository.ObtenerCategorias();
            return RespuestasUtils.GenerarRespuesta(listaDeCategorias);
        }
        // Obtener categorias activas 
        public async Task<RespuestaModel> ObtenerCategoriasActivas()
        {
            var lista = await _categoriaRepository.ObtenerCategoriasActivas();
            return RespuestasUtils.GenerarRespuesta(lista);
        }
        //Obtener categoria inactivas.
        public async Task<RespuestaModel> ObtenerCategoriasInactivas()
        {
            var lista = await _categoriaRepository.ObtenerCategoriasInactivas();
            return RespuestasUtils.GenerarRespuesta(lista);
        }

        // Crear una categoria
        public async Task<RespuestaModel> CrearCategoria(CategoriaCrearDTO dto)
        {
            var (codigo, mensaje) = await _categoriaRepository.AgregarCategoria(dto);
            return RespuestasUtils.GenerarRespuesta(codigo, mensaje);
        }
        // Deshabilitar Categoria.
        public async Task<RespuestaModel> DeshabilitarCategoria(int idCategoria)
        {
            var (codigo, mensaje) = await _categoriaRepository.DeshabilitarCategoria(idCategoria);
            return RespuestasUtils.GenerarRespuesta(codigo, mensaje);
        }
        //Actualizar Categoria.
        public async Task<RespuestaModel> ActualizarCategoria(CategoriaActualizarDTO dto)
        {
            var (codigo, mensaje) = await _categoriaRepository.ActualizarCategoria(dto);
            return RespuestasUtils.GenerarRespuesta(codigo, mensaje);
        }
        // Habilitar categoria.
        public async Task<RespuestaModel> HabilitarCategoria(int idCategoria)
        {
            var (codigo, mensaje) = await _categoriaRepository.HabilitarCategoria(idCategoria);
            return RespuestasUtils.GenerarRespuesta(codigo, mensaje);
        }






    }
}
