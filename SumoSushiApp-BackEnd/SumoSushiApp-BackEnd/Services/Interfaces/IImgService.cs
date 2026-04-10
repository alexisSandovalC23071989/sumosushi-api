namespace SumoSushiApp_BackEnd.Services.Interfaces
{
    public interface IImgService
    {
        public Task<string?> ProcesarImg(IFormFile? file);
        public Task<string?> NoUsar_PruebaObtenerExtencionImg(IFormFile? file);
    }
}
