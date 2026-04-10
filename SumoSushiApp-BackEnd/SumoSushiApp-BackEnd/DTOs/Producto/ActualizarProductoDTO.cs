using SumoSushiApp_BackEnd.DTOs.Etiqueta;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace SumoSushiApp_BackEnd.DTOs.Producto
{
    public class ActualizarProductoDTO
    {
        
        public long IdProducto { get; set; }
        public string? Nombre { get; set; }        
        public string? Descripcion { get; set; }
        public string? ImagenUrl { get; set; }
        public IFormFile? ImagenFile { get; set; }        
        public bool CanjeablePuntos { get; set; }        
        public int IdCategoria { get; set; }
        public string? Estado { get; set; }
        //public List<ActualizarEtiquetaProductoDTO> Etiquetas { get; set; } = new List<ActualizarEtiquetaProductoDTO>();
        public List<EtiquetaProductoDTO> Etiquetas { get; set; } = new List<EtiquetaProductoDTO>();
        public List<ActualizarPresentacionProductoDTO> Presentaciones { get; set; } = new List<ActualizarPresentacionProductoDTO>();

        public static ActualizarProductoDTO FromFormDto(ProductoFormDTO formDto)
        {
            var dto = JsonSerializer.Deserialize<ActualizarProductoDTO>(
                formDto.modelJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (dto == null)
                throw new InvalidOperationException("Error al deserializar el modelo");

            dto.ImagenFile = formDto.ImagenFile;
            return dto;
        }

    }
}
