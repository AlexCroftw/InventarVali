using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models.ViewModel
{
    public class AutovehiculeInvoiceVM
    {
        public int AutovehiculeID { get; set; }
        public decimal PriceFuel { get; set; }
        public double FuelConsumed { get; set; }
    }
}
