using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Utility.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure
{
    [DisallowConcurrentExecution]
    public class EmailSendBg : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyEmailSender _myEmailSender;
        private readonly IConfiguration _config;
        private readonly ILogger<EmailSendBg> _logger;
        public EmailSendBg(IUnitOfWork unitOfWork, IMyEmailSender myEmailSender, IConfiguration config, ILogger<EmailSendBg> logger)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _myEmailSender = myEmailSender;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            List<Autovehicule> objAutovehiculeslist = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            List<Invoice> objInvoiceList = _unitOfWork.Invoice.GetAll(includeProperties:"Autovehicule").ToList();
            var now = DateTime.Now;

            foreach (var invoice in objInvoiceList) 
            {
                if (invoice.InvoiceDate.HasValue) 
                {
                    TimeSpan diff = invoice.InvoiceDate.Value - now;
                    if (diff.Days <= 14 && diff.Days >= 0) 
                    {
                        _myEmailSender.SendEmail(_config.GetSection("EmailToSendTo").Value, $"Insurance Expiration Date  {now}", $"Please be informed that the DKV Invoice {invoice.InvoiceNumber} has been issued \r\n" +
                            $" And it requiers payment if you already paid it please ignore it");
                    }
                }
            }


            foreach (var date in objAutovehiculeslist)
            {
                if (date.InsuranceExpirationDate.HasValue)
                {
                    TimeSpan diff = date.InsuranceExpirationDate.Value - now;
                    if (diff.Days <= 14 && diff.Days >= 0)
                    {
                        _myEmailSender.SendEmail(_config.GetSection("EmailToSendTo").Value, $"Insurance Expiration Date  {now}", $"Please be informed that insurence for {date.LicensePlate} is expiring " +
                            $" in {diff.Days} days. Please Take Action ");
                    }
                }
                if (date.ITPExpirationDate.HasValue)
                {
                    TimeSpan diff = date.ITPExpirationDate.Value - now;
                    if (diff.Days <= 14 && diff.Days >= 0)
                    {
                        _myEmailSender.SendEmail(_config.GetSection("EmailToSendTo").Value, $"ITP Expiration  Date  {now}", $"Please be informed that ITP for {date.LicensePlate} is expiring " +
                            $" in {diff.Days} days. Please Take Action ");
                    }
                }
                if (date.VinietaExpirationDate.HasValue)
                {
                    TimeSpan diff = date.VinietaExpirationDate.Value - now;
                    if (diff.Days <= 14 && diff.Days >= 0)
                    {
                        _myEmailSender.SendEmail(_config.GetSection("EmailToSendTo").Value, $"Vinieta Expiration Date  {now}", $"Please be informed that Vinieta for {date.LicensePlate} is expiring " +
                            $" in {diff.Days} days. Please Take Action ");
                    }
                }

            }
            _logger.LogInformation($"{now}", DateTime.UtcNow);
            return Task.CompletedTask;
        }
    }
}
