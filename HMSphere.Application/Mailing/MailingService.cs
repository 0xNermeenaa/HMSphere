using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Microsoft.AspNetCore.Http;

namespace HMSphere.Application.Mailing
{
	public class MailingService : IMailingService
	{
		private readonly MailSettings _mailSettings;

		public MailingService(IOptions<MailSettings> mailSettings)
		{
			_mailSettings = mailSettings.Value;
		}

		public void SendMail(MailMessage message)
		{
			var emailMessage = CreateEmailMessage(message);
			Send(emailMessage);
		}

        public async Task SendMailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null)
        {
            //throw new NotImplementedException();
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = subject
            };
            email.To.Add(MailboxAddress.Parse(mailTo));
            var builder = new BodyBuilder();
            if (attachments != null) 
             {
                byte[] fileBytes;
                foreach (var file in attachments)
                { 
                  if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();

                        builder.Attachments.Add(file.FileName,fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
             }

            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.DisplayName, _mailSettings.Password);
            await smtp.SendAsync(email);    

            smtp.Disconnect(true);
        }


        private MimeMessage CreateEmailMessage(MailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("HMS", _mailSettings.Email));  // Changed "email" to "sender"
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

			var bodyBuilder = new BodyBuilder
			{
				HtmlBody = $"<p><img src=\"cid:logo\" alt=\"CMS\" style=\"vertical-align:middle;\" /> <span style=\"vertical-align:middle;\">Hospital Management System</span></p>" +
					   $"{message.Content}"
			};

			emailMessage.Body = bodyBuilder.ToMessageBody();
			return emailMessage;
		}

        private void Send(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_mailSettings.Host, _mailSettings.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_mailSettings.DisplayName, _mailSettings.Password);
                    client.Send(message);
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
    }




}
