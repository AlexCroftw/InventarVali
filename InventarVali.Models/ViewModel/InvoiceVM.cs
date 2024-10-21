using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace InventarVali.Models.ViewModel
{
    public class InvoiceVM
    {
        public int Id { get; set; }
        [Required]
        [Remote(action: "VerifyInvoiceNumber", controller: "invoice", AdditionalFields = "Id")]
        [RegularExpression(@"^\d{2}/\d{9}/\d{3}$", ErrorMessage ="The pattern is xx\\xxxxxxxxx\\xxx")]
        public string? InvoiceNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime? InvoiceDate { get; set; }
        [DataType(DataType.Date)]
        public string? DisplayInvoiceDate { get; set; }
        [DataType(DataType.Currency)]
        public double? TotalPrice { get; set; }
        public string? CardNumber { get; set; }
        [ValidateNever]
        public string? InvoiceUrl { get; set; }
        [ValidateNever]
        public List<AutovehiculeInvoiceVM> AutovehiculeInvoice { get; set; } = new List<AutovehiculeInvoiceVM>();
        [ValidateNever]
        public List<AutovehiculeVM> Autovehicule { get; set; }
    }
}
