using Microsoft.AspNetCore.Http;

namespace HMSphere.Application.Mailing

{
    public interface IMailingService
    {
        Task SendMailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null);
    }
}
