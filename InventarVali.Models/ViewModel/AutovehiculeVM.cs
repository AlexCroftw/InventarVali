
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventarVali.Models.ViewModel
{
    public class AutovehiculeVM
    {
        public int Id { get; set; }
        public string Type { get; set; }
        [MaxLength(10)]
        [DisplayName("License Plate")]
        [Remote(action:"VerifyPlate",controller:"autovehicule")]
        public string LicensePlate { get; set; }
        [DisplayName("VIN Number")]
        [MaxLength(17)]
        [Remote(action: "VerifyVin", controller: "autovehicule")]
        public string VinNumber { get; set; }

        [DisplayName("Insurance Start Date")]

        public string ? DisplayInsurenceDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime InsurenceDate { get; set; }
        [DisplayName("ITP")]
        public string? DisplayHasITP { get; set; }
        public bool HasITP { get; set; }

        [DisplayName("ITP Expiration Date")]
        public string? DisplayITPExpirationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ITPExpirationDate { get; set; }

        [DisplayName("Insurance Expiration Date")]
        public string? DisplayInsuranceExpirationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime InsuranceExpirationDate { get; set; }
        [DisplayName("Vinieta")]
        public string? DisplayHasVinieta { get; set; }
        public bool HasVinieta { get; set; }

        [DisplayName("Vinieta Expiration Date")]
        public string? DisplayVinietaExpirationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime VinietaExpirationDate { get; set; }
        public int? EmployeeId { get; set; }

        public string? ImageUrl { get; set; }
        [ValidateNever]
        public EmployeesVM Employees { get; set; }
        [DisplayName("Insurence Doc")]
        public string? InsurenceDoc { get; set; }

    }
}

