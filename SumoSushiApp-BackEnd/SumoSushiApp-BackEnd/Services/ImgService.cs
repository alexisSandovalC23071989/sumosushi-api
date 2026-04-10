using SumoSushiApp_BackEnd.Services.Interfaces;
using System.Reflection;

namespace SumoSushiApp_BackEnd.Services
{
    public class ImgService : IImgService
    {
        private static readonly string[] ImgPrueba =
        {
            "https://japanesetaste.co.uk/cdn/shop/articles/how-to-make-makizushi-sushi-rolls-japanese-taste.jpg",
            "https://www.oliveradatenea.com/wp-content/uploads/2023/06/Sushi-rolls-de-salmon-y-Olivada-Olivera-dAtenea.jpg",
            "https://tildaricelive.s3.eu-central-1.amazonaws.com/wp-content/uploads/sites/29/2023/05/09144414/Maki-roll-invertido-relleno-de-aguacate-langostino-en-tempura-y-pepino_K2F6221.jpg",
            "https://www.tresjotas.com/wp-content/uploads/2021/08/Kit-basico-para-preparar-sushi-en-casa.jpg",
            "https://content-cocina.lecturas.com/medio/2018/07/19/sushi-variado-tradicional_91be2c41_800x800.jpg",
            "https://www.sushigardenwatsonville.com/wp-content/uploads/2020/08/2item-lunch-bento_1.jpg",
            "https://www.justonecookbook.com/wp-content/uploads/2019/02/Meal-Prep-Bento-I-2.jpg",
            "https://cdn.shopify.com/s/files/1/0639/9344/6656/files/miso_1024x1024.jpg",
            "https://assets.tmecosys.com/image/upload/t_web_rdp_recipe_584x480_1_5x/img/recipe/ras/Assets/1c070466-279d-4906-87ee-c4ee0492d022/Derivates/1e431b1c-2394-44a5-b3bb-83b199e4b432.jpg",
            "https://thebigmansworld.com/wp-content/uploads/2024/01/mediterranean-bowl-recipe.jpg"
            };

        private readonly Random _random = new Random();

        public async Task<string?> ProcesarImg(IFormFile? file)
        {
            await Task.Delay(500);  

            if (file == null || file.Length == 0)
                return null;

            return ImgPrueba[_random.Next(ImgPrueba.Length)];
        }

        public async Task<string?> NoUsar_PruebaObtenerExtencionImg(IFormFile? file)
        {
            await Task.Delay(500);

            if (file == null || file.Length == 0)
                return "Archivo no válido";

            return file.ContentType; // Devuelve el tipo MIME del archivo
        }

        // *** METODO PARA ESTANDARIZAR LA IMAGEN 
    }
}
