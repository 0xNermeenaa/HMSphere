namespace HMSphere.Application.Mailing
{
	public interface IMailingService
	{
		void SendMail(MailMessage message);
	}
}
