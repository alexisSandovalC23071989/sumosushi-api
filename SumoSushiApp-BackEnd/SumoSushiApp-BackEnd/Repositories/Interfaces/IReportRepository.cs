using SumoSushiApp_BackEnd.DTOs.Reporte;

namespace SumoSushiApp_BackEnd.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<ProductPerformanceDto>> GetProductPerformance(DateTime inicio, DateTime fin, PeriodoReporte periodo);
        Task<GeneralPerformanceDto> GetGeneralPerformance(DateTime inicio, DateTime fin, PeriodoReporte periodo);
        Task<IEnumerable<MetodoPagoUsoDto>> GetMetodoPagoUso(DateTime inicio, DateTime fin, PeriodoReporte periodo);
        Task<IEnumerable<TipoPedidoUsoDto>> GetTipoPedidoUso(DateTime inicio, DateTime fin, PeriodoReporte periodo);
    }
}