using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string VinNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime? InsurenceDate { get; set; }

        public bool? HasITP { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ITPExpirationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? InsuranceExpirationDate { get; set; }

        public bool? HasVinieta { get; set; }
        [DataType(DataType.Date)]
        public DateTime? VinietaExpirationDate { get; set; }
        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        [ValidateNever]
        public Employees Employees { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }

    }
}
