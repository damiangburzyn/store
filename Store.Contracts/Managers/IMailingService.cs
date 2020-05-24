using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Store.Contracts
{
    public interface IMailingService
    {
        Task<bool> SendContactUsMailToSender(string recipient, string message, IFormFile attachment = null);

        Task<bool> SendContactUsMailToReceiver(string recipient, string message, IFormFile attachment = null);

        Task<bool> SendVerificationCodeMail(string recipient, string callbackUrl);

        Task<bool> SendForgotPasswordCodeMail(string recipient, string callbackUrl);
      
        Task<bool> SendSuccessfullRegistrationMail(string email);

        Task<bool> SendMail(string recipient, string title , string message, IFormFile attachment = null);
    }
}