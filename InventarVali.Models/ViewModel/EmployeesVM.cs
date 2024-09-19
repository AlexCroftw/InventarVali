using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventarVali.Models.ViewModel
{
    public class EmployeesVM
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [ValidateNever]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

    }
}
