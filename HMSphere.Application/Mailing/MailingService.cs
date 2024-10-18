using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

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

		private MimeMessage CreateEmailMessage(MailMessage message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress("HMS", _mailSettings.From));  // Changed "email" to "sender"
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
					client.Connect(_mailSettings.SmtpServer, _mailSettings.Port, true);
					client.AuthenticationMechanisms.Remove("XOAUTH2");
					client.Send(message);
					client.Authenticate(_mailSettings.Username, _mailSettings.Password);
				}
				finally
				{
					client.Disconnect(true);
				}
			}
		}
	}




}
