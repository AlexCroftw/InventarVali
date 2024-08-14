using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models
{
    public class Autovehicule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public string VinNumber { get; set; }
        [Required]
        public DateTime InsurenceDate { get; set; }
        [Required]
        public bool HasITP { get; set; }
    }
}
