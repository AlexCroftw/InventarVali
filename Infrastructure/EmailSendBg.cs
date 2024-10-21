using AutoMapper;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
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
        private readonly IMapper _mapper;
        public EmailSendBg(IUnitOfWork unitOfWork, IMyEmailSender myEmailSender, IConfiguration config, ILogger<EmailSendBg> logger,
                           IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _myEmailSender = myEmailSender;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var wwwRoot = _webHostEnvironment.WebRootPath;
            List<Autovehicule> objAutovehiculeslist = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            List<Invoice> objInvoiceList = _unitOfWork.Invoice.GetAll(includeProperties: "AutovehiculeInvoice").ToList();
            var autovehiculeVM = _mapper.Map<List<AutovehiculeVM>>(objAutovehiculeslist);
            var invoiceVM = _mapper.Map<List<InvoiceVM>>(objInvoiceList);

            foreach (var item in invoiceVM) 
            {
                item.Autovehicule = autovehiculeVM;
            }



            var now = DateTime.Now;

            foreach (var invoice in invoiceVM)
            {
                if (invoice.InvoiceDate.HasValue)
                {
                    TimeSpan diff = invoice.InvoiceDate.Value - now;

                    // Only proceed for invoices due in the next 14 days
                    if (diff.Days <= 14 && diff.Days >= 0)
                    {

                        var emailBody = $"Please be informed that the DKV Invoice {invoice.InvoiceNumber} has been issued. \r\n";
                        emailBody += $"It requires payment, and the amount due is {invoice.TotalPrice} RON.\r\n";
                        emailBody += "If you already paid it, please ignore this message.\r\n";


                        emailBody += "Here are the details for each vehicle in this invoice:\r\n";

                        foreach (var autovehiculeInvoice in invoice.AutovehiculeInvoice)
                        {

                            var autovehicule = autovehiculeVM.FirstOrDefault(a => a.Id == autovehiculeInvoice.AutovehiculeID);

                            if (autovehicule != null)
                            {
                                emailBody += $"- License Plate: {autovehicule.LicensePlate}, Fuel Price: {autovehiculeInvoice.PriceFuel} RON , Fuel Consumed: {autovehiculeInvoice.FuelConsumed} l \r\n";
                            }
                        }

                        var emailRecipient = _config.GetSection("EmailToSendTo").Value;
                        var emailSubject = $"Invoice DKV {now}";
                        var invoiceFilePath = Path.Combine(wwwRoot, @"invoice", $"{invoice.InvoiceNumber}.pdf");

                        _myEmailSender.SendEmailWithAttachment(emailRecipient, emailSubject, emailBody, invoiceFilePath);
                    }
                }
            }
        

            foreach (var date in autovehiculeVM)
            {
                if (date.InsuranceExpirationDate != null)
                {
                    TimeSpan diff = date.InsuranceExpirationDate - now;
                    if (diff.Days <= 14 && diff.Days >= 0)
                    {
                        _myEmailSender.SendEmail(_config.GetSection("EmailToSendTo").Value, $"Insurance Expiration Date  {now}", $"Please be informed that insurence for {date.LicensePlate} is expiring " +
                            $" in {diff.Days} days. Please Take Action ");
                    }
                }
                if (date.ITPExpirationDate != null)
                {
                    TimeSpan diff = date.ITPExpirationDate - now;
                    if (diff.Days <= 14 && diff.Days >= 0)
                    {
                        _myEmailSender.SendEmail(_config.GetSection("EmailToSendTo").Value, $"ITP Expiration  Date  {now}", $"Please be informed that ITP for {date.LicensePlate} is expiring " +
                            $" in {diff.Days} days. Please Take Action ");
                    }
                }
                if (date.VinietaExpirationDate != null)
                {
                    TimeSpan diff = date.VinietaExpirationDate - now;
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
