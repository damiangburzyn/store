using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Store.Contracts;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Store.Services
{
    public class MailSenderManager : IMailingService
    {
        private readonly IStringLocalizer<MailSenderManager> _localizer;
        private readonly Mailing _mailingOptions;
        private readonly ILogger<MailSenderManager> _logger;


        public MailSenderManager(
            IStringLocalizer<MailSenderManager> localizer,
            IOptions<AppSettings> appsettings,
            ILogger<MailSenderManager> logger)
        {
            _localizer = localizer;
            _mailingOptions = appsettings.Value.Mailing;
            _logger = logger;
          
        }

        public MailSenderManager(
            IOptions<Mailing> mailingOptions)
        {
            _mailingOptions = mailingOptions.Value;

           
        }

        public async Task<bool> SendContactUsMailToSender(string recipient, string message, IFormFile attachment = null)
        {
            try
            {
                var title = "Wiadomość z formularza kontaktowego";
                var senderContactMessagePart ="Oto wiadomośc wysłana przez Ciebie: ";
                message = $"{senderContactMessagePart} {message}";

                await SendMail(recipient, title, message, _mailingOptions.Senders.Keys.First(), attachment, string.Empty);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd wysyłąnia wiadomości z formularza kontaktu", null);
                return false;
            }

        }

        public async Task<bool> SendContactUsMailToReceiver(string recipient, string message, IFormFile attachment = null)
        {
            try
            {
                var title = "Wiadomość z formularza kontaktowego";
                var receiverContactMessagePart = "Oto wiadomość wysłana w formularzu kontaktowym";
                message = $"{receiverContactMessagePart} {message}";
                await SendMail(recipient, title, message, _mailingOptions.Senders.Keys.First(), attachment, string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd wysyłąnia kopii wiadomości z formularza kontaktu do osoby wysyłającej", null);
                return false;
            }
        }

        public async Task<bool> SendVerificationCodeMail(string recipient, string callbackUrl)
        {
            try
            {
                var title = "Potwierdź swoje konto";
                var body = string.Format("Otrzymałeś tę wiadomość, ponieważ odnotowaliśmy, że chcesz zarejestrować się w naszym serwisie; aby ukończyć proces rejestracji, kliknij w poniższy link, w przeciwnym razie zignoruj tę wiadomość. <a href='{0}'>link</a>", callbackUrl);

                await SendMail(recipient, title, body, string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd wysyłąnia wiadomości z linkiem aktywacyjnym", null);
                return false;
            }
        }

        public async Task<bool> SendForgotPasswordCodeMail(string recipient, string callbackUrl)
        {
            try
            {
                var title = "Reset hasła";
                var body = string.Format("Otrzymałeś tę wiadomość, ponieważ odnotowaliśmy, że chcesz zarejestrować się w naszym serwisie; aby ukończyć proces rejestracji, kliknij w poniższy link, w przeciwnym razie zignoruj tę wiadomość. <a href='{0}'>link</a>", callbackUrl);

                await SendMail(recipient, title, body, string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd wysyłąnia wiadomości z przypomieniem hasła", null);
                return false;
            }
        }


        private SmtpClient GetMailClient(string sender)
        {
            if (string.IsNullOrEmpty(sender) || !_mailingOptions.Senders.ContainsKey(sender))
            {
                throw new ArgumentException("Sender name is not valid");
            }

            var smtpClient = new SmtpClient
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };

            smtpClient.Connect(_mailingOptions.MailHost, _mailingOptions.MailPort, _mailingOptions.UseSsl);

            if (!_mailingOptions.IsAuthenticationDisabled)
            {
                smtpClient.Authenticate(string.Join("@", sender, _mailingOptions.SenderDomain), _mailingOptions.Senders[sender]);
            }
            return smtpClient;
        }


        public async Task SendMail(string recipient, string subject, string body,  string sender = null, IFormFile file = null, string externalSender = null)
        {
            if (_mailingOptions.IsDisabled)
            {
                return;
            }

            var message = new MimeMessage();

            if (!string.IsNullOrEmpty(externalSender))
            {
                message.ReplyTo.Add(new MailboxAddress(externalSender, externalSender));
            }
            if (string.IsNullOrEmpty(sender))
            {
                sender = _mailingOptions.Senders.Keys.First();
            }

            message.From.Add(new MailboxAddress(_mailingOptions.SenderDomain, string.Join("@", sender, _mailingOptions.SenderDomain)));
            message.To.Add(new MailboxAddress(recipient, recipient));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body + $"<br/><br/><div>{"Pozdrawiamy,<br/>"}</div>"
            };

            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    bodyBuilder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = GetMailClient(sender))
            {
                await client.SendAsync(message);

                client.Disconnect(true);
            }
        }

       

        public async Task<bool> SendSuccessfullRegistrationMail(string recipient)
        {
            try
            {
                var title = "Proces rejestracji zakończony sukcesem";
                var body = "Gratulacje. Zostałeś zarejestrowany w naszej witrynie.";
                await SendMail(recipient, title, body,  _mailingOptions.Senders.Keys.First());
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd wysyłąnia powiadomienia o pozytywnej rejestracji do użytkownika", null);
                return false;
            }
        }


        public async Task<bool> SendMail(string recipient, string title, string message, IFormFile attachment = null) {

            try
            {

                await SendMail(recipient, title, message, _mailingOptions.Senders.Keys.First(), attachment, string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd wysyłąnia email", null);
                return false;
            }
        }


    }
}
