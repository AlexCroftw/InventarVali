using InventarVali.Utility.Services;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System.Net.Mime;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace InventarVali.Utility
{
    public class MyEmailSender : IMyEmailSender
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MyEmailSender(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }

        public void SendEmail(string To, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(To));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_config.GetSection("EmailHost").Value, 465, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            };

        }

        public void SendEmailWithAttachment(string To, string subject, string htmlMessage, string fileName)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(To));
            email.Subject = subject;
            var textPart = new TextPart(TextFormat.Html)
            {
                Text = htmlMessage,
                ContentTransferEncoding = ContentEncoding.Base64,
            };

            var attachment = new MimePart(MediaTypeNames.Application.Pdf)
            {
                Content = new MimeContent(File.OpenRead(fileName)),
                ContentId = fileName,
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = fileName,
            };

            var multipart = new Multipart
            {
                textPart,
                attachment
            };
            email.Body = multipart;

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_config.GetSection("EmailHost").Value, 465, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            };
        }
    }
}
