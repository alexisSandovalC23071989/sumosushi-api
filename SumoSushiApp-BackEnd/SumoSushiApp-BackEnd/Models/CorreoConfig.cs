namespace SumoSushiApp_BackEnd.Models
{
    public class CorreoConfig
    {
        public string? FromEmail { get; set; }
        public string? AppPassword { get; set; }
        public int SmtpPort { get; set; }
        public string? SmtpHost { get; set; }
        public bool EnableSsl { get; set; }
    }
}
