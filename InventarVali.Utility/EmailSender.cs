using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration config)
        {
            _config = config;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {   
            //return Task.CompletedTask;
            System.Net.Mail.SmtpClient client = new System.Net.Mail. SmtpClient
            {
                Port = 465,
                Host =  _config.GetSection("EmailHost").Value, //or another email sender provider
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value)
            };

            return client.SendMailAsync(_config.GetSection("EmailUsername").Value, email, subject, message);

        }
    }
}
