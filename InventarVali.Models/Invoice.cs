using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventarVali.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal Price { get; set; }
        public string? CardNumber { get; set; }
        public string? InvoiceUrl { get; set; }
        [ValidateNever]
        public List<AutovehiculeInvoice> AutovehiculeInvoice { get; set; }

    }
}
