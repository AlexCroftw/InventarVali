using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Utility.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;


namespace Infrastructure
{
    [DisallowConcurrentExecution]
    public class EmailSendBg : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMyEmailSender _myEmailSender;
        private readonly IConfiguration _config;
        private readonly ILogger<EmailSendBg> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmailSendBg(IUnitOfWork unitOfWork, IMyEmailSender myEmailSender, IConfiguration config, ILogger<EmailSendBg> logger,
                           IWebHostEnvironment webHostEnvironment)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _myEmailSender = myEmailSender;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var wwwRoot = _webHostEnvironment.WebRootPath;
            List<Autovehicule> objAutovehiculeslist = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            List<Invoice> objInvoiceList = _unitOfWork.Invoice.GetAll(includeProperties: "AutovehiculeInvoice").ToList();
            var now = DateTime.Now;

            foreach (var invoice in objInvoiceList)
            {
                if (invoice.InvoiceDate.HasValue)
                {
                    TimeSpan diff = invoice.InvoiceDate.Value - now;
                    if (diff.Days <= 14 && diff.Days >= 0)
                    {
                        _myEmailSender.SendEmailWithAttachment(_config.GetSection("EmailToSendTo").Value, $"Invoice DKV  {now}", $"Please be informed that the DKV Invoice {invoice.InvoiceNumber} has been issued \r\n" +
                            $" And it requiers payment if you already paid it please ignore it, the ammount due is {invoice.TotalPrice} RON", Path.Combine(wwwRoot, @"invoice", $"{invoice.InvoiceNumber}.pdf"));
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
