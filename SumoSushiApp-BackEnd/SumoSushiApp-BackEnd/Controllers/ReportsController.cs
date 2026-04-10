using Microsoft.AspNetCore.Mvc;
using SumoSushiApp_BackEnd.DTOs.Reporte;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SumoSushiApp_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _repo;
        public ReportsController(IReportRepository repo)
            => _repo = repo;

        [HttpGet("ProductPerformance")]
        public async Task<IActionResult> ProductPerformance(
            [FromQuery] DateTime inicio,
            [FromQuery] DateTime fin,
            [FromQuery] PeriodoReporte periodo)
        {
            var data = await _repo.GetProductPerformance(inicio, fin, periodo);
            return Ok(data);
        }

        [HttpGet("GeneralPerformance")]
        public async Task<IActionResult> GeneralPerformance(
            [FromQuery] DateTime inicio,
            [FromQuery] DateTime fin,
            [FromQuery] PeriodoReporte periodo)
        {
            // Llamadas directas al repositorio
            var general = await _repo.GetGeneralPerformance(inicio, fin, periodo);
            var metodos = await _repo.GetMetodoPagoUso(inicio, fin, periodo);
            var tipos = await _repo.GetTipoPedidoUso(inicio, fin, periodo);

            return Ok(new
            {
                general,
                MetodosPago = metodos,
                TipoPedido = tipos
            });
        }
    }
}
