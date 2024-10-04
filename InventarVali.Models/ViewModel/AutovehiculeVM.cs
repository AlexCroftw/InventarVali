using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventarVali.Models.ViewModel
{
    public class AutovehiculeVM
    {
        public int Id { get; set; }

        public string Type { get; set; }
        [DisplayName("License Plate")]
        public string LicensePlate { get; set; }
        [DisplayName("VIN Number")]
        public string VinNumber { get; set; }

        [DisplayName("Insurance Start Date")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public string ?InsurenceDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DisplayInsurenceDate { get; set; }
        [DisplayName("ITP")]
        public string? HasITP { get; set; }

        [DisplayName("ITP Expiration Date")]
        public string? ITPExpirationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DisplayITPExpirationDate { get; set; }

        [DisplayName("Insurance Expiration Date")]
        public string? InsuranceExpirationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DisplayInsuranceExpirationDate { get; set; }
        [DisplayName("Vinieta")]
        public string? HasVinieta { get; set; }

        [DisplayName("Vinieta Expiration Date")]
        public string? VinietaExpirationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DisplayVinietaExpirationDate { get; set; }
        public int? EmployeeId { get; set; }

        public string? ImageUrl { get; set; }
        [ValidateNever]
        public EmployeesVM Employees { get; set; }
        [DisplayName("Insurence Doc")]
        public string? InsurenceDoc { get; set; }

    }
}

