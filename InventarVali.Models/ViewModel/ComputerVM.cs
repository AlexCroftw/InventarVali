using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

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
