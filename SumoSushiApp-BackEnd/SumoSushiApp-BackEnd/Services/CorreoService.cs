using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SumoSushiApp_BackEnd.DTOs.Auth;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace SumoSushiApp_BackEnd.Services
{
    public class CorreoService : ICorreoService
    {
        private readonly CorreoConfig _config;

        public CorreoService(IOptions<CorreoConfig> config) 
        {
            _config = config.Value;
            if (string.IsNullOrEmpty(_config.FromEmail) || string.IsNullOrEmpty(_config.AppPassword))
                throw new ArgumentException("CorreoConfig no está correctamente configurado.");
        }

        public async Task<bool> EnviarCorreo(string toEmail, string subject, string body, bool isHtml = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(toEmail))
                    throw new ArgumentException("El correo de destino no puede estar vacío.", nameof(toEmail));

                using var smtpClient = new SmtpClient(_config.SmtpHost)
                {
                    Port = _config.SmtpPort,
                    Credentials = new NetworkCredential(_config.FromEmail, _config.AppPassword),
                    EnableSsl = _config.EnableSsl,
                };

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(_config.FromEmail!),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml,
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
                // Aquí podrías loguear el éxito si tienes un sistema de logging.
                return true;
            }
            catch (Exception ex)
            {
                // Loguea el error (puedes inyectar ILogger si estás en ASP.NET Core, por ejemplo)
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                // También podrías registrar `ex.ToString()` si deseas más detalle.
                return false;
            }
        }


        public async Task<string> GenerarCorreo(AuthTokenRecuperacionDTO model)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Plantillas", "CorreoRecuperacionContrasenna.html");

            if (!File.Exists(templatePath))
                throw new FileNotFoundException("La plantilla de correo no fue encontrada.", templatePath);

            var htmlTemplate = await File.ReadAllTextAsync(templatePath);

            string fechaFormateada = model.FechaExpiracion.ToString("dd/MM/yyyy HH:mm");

            var cuerpoCorreo = htmlTemplate
                .Replace("{{TOKEN}}", model.Token ?? "[token no disponible, vuelve a solicitar otro]")
                .Replace("{{EXPIRA}}", fechaFormateada);

            return cuerpoCorreo;
        }
    }
}