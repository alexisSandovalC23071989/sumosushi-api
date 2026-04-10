using SumoSushiApp_BackEnd.DTOs.Promocion;
﻿using Microsoft.IdentityModel.Tokens;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services.Interfaces;
using SumoSushiApp_BackEnd.Services.Utils;

namespace SumoSushiApp_BackEnd.Services
{
    public class PromocionService : IPromocionService
    {
        private readonly IPromocionRepository _repo;
        private readonly IImgService _imgSvc;

        public PromocionService(IPromocionRepository repo, IImgService imgSvc)
        {
            _repo = repo;
            _imgSvc = imgSvc;
        }

        public async Task<RespuestaModel> ObtenerTodasPromociones()
        {
            var lista = await _repo.ObtenerTodasAsync();
            return RespuestasUtils.GenerarRespuesta(lista);
        }

        public async Task<RespuestaModel> ObtenerPromocionesActivas()
        {
            var lista = await _repo.ObtenerActivasAsync();
            return RespuestasUtils.GenerarRespuesta(lista);
        }

        public async Task<RespuestaModel> ObtenerPromocionesInactivas()
        {
            var lista = await _repo.ObtenerInactivasAsync();
            return RespuestasUtils.GenerarRespuesta(lista);
        }

        public async Task<RespuestaModel> CrearPromocion(PromocionCrearDTO dto)
        {
            // Se sube la imagen y recibe la URL
            dto.ImagenUrl = await _imgSvc.ProcesarImg(dto.ImagenFile);

            // ** VERIFICAR QUE SE RECIBIO UNA URL ** 

            // Se llama al repositorio, que ya mapeará ImagenUrl (string) y omitirá ImagenFile
            var (cod, msg) = await _repo.CrearPromocionAsync(dto);
            return RespuestasUtils.GenerarRespuesta(cod, msg);
        }

        public async Task<RespuestaModel> ActualizarPromocion(PromocionActualizarDTO dto)
        {
              
            var nuevaImagenUrl = "";

            if(dto.ImagenFile is not null)
                nuevaImagenUrl = await _imgSvc.ProcesarImg(dto.ImagenFile);

            if(!nuevaImagenUrl.IsNullOrEmpty())
                dto.ImagenUrl = await _imgSvc.ProcesarImg(dto.ImagenFile);

            var (cod, msg) = await _repo.ActualizarPromocionAsync(dto);
            return RespuestasUtils.GenerarRespuesta(cod, msg);
        }

        public async Task<RespuestaModel> ActivarPromocion(PromocionCambiarEstadoDTO dto)
        {
            var (cod, msg) = await _repo.ActivarPromocionAsync(dto);
            return RespuestasUtils.GenerarRespuesta(cod, msg);
        }

        public async Task<RespuestaModel> DeshabilitarPromocion(PromocionCambiarEstadoDTO dto)
        {
            var (cod, msg) = await _repo.DeshabilitarPromocionAsync(dto);
            return RespuestasUtils.GenerarRespuesta(cod, msg);
        }
    }
}