using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InventarVali.Models.ViewModel
{
    public class AutovehiculeVM
    {
        public int Id { get; set; }

        public string Type { get; set; }
 
        public string LicensePlate { get; set; }

        public string VinNumber { get; set; }

        public string InsurenceDate { get; set; }

        public bool? HasITP { get; set; }
        [DataType(DataType.Date)]
        public string? ITPExpirationDate { get; set; }
        [DataType(DataType.Date)]
        public string? InsuranceExpirationDate { get; set; }

        public bool? HasVinieta { get; set; }
        [DataType(DataType.Date)]
        public string? VinietaExpirationDate { get; set; }
        public int? EmployeeId { get; set; }

        public string? ImageUrl { get; set; }
        [ValidateNever]
        public EmployeesVM Employees { get; set; }    
        
    }
}

