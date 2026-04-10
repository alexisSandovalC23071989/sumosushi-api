using Dapper;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.DTOs.Reporte;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using System.Data;

namespace SumoSushiApp_BackEnd.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDBContext _dbContext;

        public ReportRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<IEnumerable<ProductPerformanceDto>> GetProductPerformance(DateTime inicio, DateTime fin, PeriodoReporte periodo)
        {
            using var conexion = _dbContext.MySqlConexion();
            return await conexion.QueryAsync<ProductPerformanceDto>(
                "ReporteProductos",
                new { p_fecha_inicio = inicio, p_fecha_fin = fin, p_periodo = periodo.ToString() },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<GeneralPerformanceDto> GetGeneralPerformance(DateTime inicio, DateTime fin, PeriodoReporte periodo)
        {
            using var conexion = _dbContext.MySqlConexion();
            using var multi = await conexion.QueryMultipleAsync(
                "ReporteGeneral",
                new { p_fecha_inicio = inicio, p_fecha_fin = fin, p_periodo = periodo.ToString() },
                commandType: CommandType.StoredProcedure
            );
            var general = await multi.ReadFirstAsync<GeneralPerformanceDto>();
            var metodos = await multi.ReadAsync<MetodoPagoUsoDto>();
            var tipos = await multi.ReadAsync<TipoPedidoUsoDto>();

            return general;
        }

        public Task<IEnumerable<MetodoPagoUsoDto>> GetMetodoPagoUso(DateTime inicio, DateTime fin, PeriodoReporte periodo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TipoPedidoUsoDto>> GetTipoPedidoUso(DateTime inicio, DateTime fin, PeriodoReporte periodo)
        {
            throw new NotImplementedException();
        }

        // Métodos GetMetodoPagoUso y GetTipoPedidoUso pueden consumir multi.ReadAsync directamente
    }
}
