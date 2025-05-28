using Commun.Helpers;
using Commun.Logger;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using ServiceLayer.IService;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceLayer.Service
{
    public class SendMailService : ISendMailService
    {
        private readonly IConfiguration configuration;
        private readonly ICreateLogger createLogger;

        private readonly string _apiKey;
        private readonly string _from;

        public SendMailService(IConfiguration _configuration, ICreateLogger createLogger)
        {
            this.configuration = _configuration;
            this.createLogger = createLogger;
           
            _apiKey = SendGridSettings.SendGrid_Key;
            _from = SendGridSettings.SendGrid_fromMailSendGrid;
        }

        public async Task<bool> EnviarMail(Persona persona, MensajeModel mensaje)
        {
            bool vRetorno = false;

            try
            {
                string text = File.ReadAllText(this.configuration.GetSection("Settings").GetSection("EMAIL_TEMPLATE_MENSAJE").Value, Encoding.UTF8);

                text = text.Replace("{P_NOMBRE}", persona.Nombres + " " + persona.Apellidos);
                text = text.Replace("{P_CELULAR}", persona.Telefono);
                text = text.Replace("{P_EMAIL}", persona.Email);
                text = text.Replace("{P_ASUNTO}", mensaje.Asunto);
                text = text.Replace("{P_MENSAJE}", mensaje.Observacion);

                var client = new SendGridClient(_apiKey);
                var from = new EmailAddress(_from);
                var to = new EmailAddress(persona.Email);
                var subject = this.configuration.GetSection("Settings").GetSection("SUBJECT_MAIL_REG_USER").Value;
                var plainTextContent = Regex.Replace(text, "<[^>]*>", "");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, text);
                var response = client.SendEmailAsync(msg);

                vRetorno = true;
            }
            catch (Exception ex)
            {
                createLogger.LogWriteExcepcion(ex.Message);
            }

            return vRetorno;
        }
    }
}
