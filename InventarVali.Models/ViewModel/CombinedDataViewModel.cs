using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models.ViewModel
{
    public class CombinedDataViewModel
    {
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        public string FullName { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string? SerialNumber { get; set; }
    }
}
