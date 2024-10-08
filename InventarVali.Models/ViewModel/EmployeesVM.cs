using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventarVali.Models.ViewModel
{
    public class EmployeesVM
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        [StringLength(10)]
        [RegularExpression(@"^[A-Za-z]{1,10}$",
            ErrorMessage ="Name must not contain characters or digits")]
        [Remote(action: "VerifyName", controller:"employee", AdditionalFields ="LastName,Id")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [StringLength(10)]
        [RegularExpression(@"^[A-Za-z]{1,10}$",
            ErrorMessage = "Name must not contain characters or digits")]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Remote(action: "VerifyEmail", controller:"employee",AdditionalFields ="Id")]
        public string? Email { get; set; }

        [DisplayName("Full Name")]
        [ValidateNever]
        public string FullName { get; set; }

    }
}
