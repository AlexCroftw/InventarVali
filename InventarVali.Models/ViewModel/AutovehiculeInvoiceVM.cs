using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models.ViewModel
{
    public class AutovehiculeInvoiceVM
    {
        public Autovehicule Autovehicule { get; set; }
        public Invoice Invoice { get; set; }
        public int AutovehiculeFKID { get; set; }
        public int InvoiceFKID { get; set; }
        public decimal PriceFuel { get; set; }
        public double FuelConsumed { get; set; }
    }
}
