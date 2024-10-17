namespace InventarVali.Utility.Services
{
    public interface IMyEmailSender
    {
        void SendEmail(string email, string subject, string htmlMessage);

        void SendEmailWithAttachment(string email, string subject, string htmlMessage, string attachment);
    }
}
