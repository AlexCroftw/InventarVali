using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models.ViewModel
{
    public  class InvoiceVM
    {
        public int Id { get; set; }
        public string? InvoiceNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime? InvoiceDate { get; set; }
        [DataType(DataType.Date)]
        public string? DisplayInvoiceDate { get; set; }
        [DataType(DataType.Currency)]
        public decimal? TotalPrice { get; set; }
        [DataType(DataType.Currency)]
        public List<decimal?> Price { get; set; }
        public string ?CardNumber { get; set; }
        [ValidateNever]
        public string? InvoiceUrl { get; set; }
        [ValidateNever]
        public List <AutovehiculeVM> Autovehicule { get; set; }
    }
}
