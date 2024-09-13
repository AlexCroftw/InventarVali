using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models.ViewModel
{
    public class ComputerVM
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        [DisplayName("Serial Number")]
        public string? SerialNumber { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }
        public int? EmployeeId { get; set; }
        [ValidateNever]
        public EmployeesVM Employee { get; set; }
    }
}
