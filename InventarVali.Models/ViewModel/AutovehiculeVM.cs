using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models.ViewModel
{
    public class AutovehiculeVM
    {
        public int Id { get; set; }

        public string Type { get; set; }
 
        public string LicensePlate { get; set; }

        public string VinNumber { get; set; }

        public DateTime? InsurenceDate { get; set; }

        public bool? HasITP { get; set; }

        public DateTime? ITPExpirationDate { get; set; }

        public DateTime? InsuranceExpirationDate { get; set; }

        public bool? HasVinieta { get; set; }

        public DateTime? VinietaExpirationDate { get; set; }

        public string? ImageUrl { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
    }
}
///TODO  Create a automapper situation
