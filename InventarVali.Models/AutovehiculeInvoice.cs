using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models
{
    public  class AutovehiculeInvoice
    {
        public Autovehicule Autovehicule { get; set; }
        public Invoice Invoice { get; set; }
        public  int AutovehiculeId { get; set; }
        public int InvoiceId { get; set; }
        public decimal PriceFuel { get; set; }
        public double FuelConsumed { get; set; }
    }
}
