using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models.ViewModel
{
    public class EmployeesVM
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string? Email { get; set; }
        [ValidateNever]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
       
    }
}
