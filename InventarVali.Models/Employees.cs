using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventarVali.Models
{
    public class Employees
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        [ValidateNever]
        public string FullName { get; set; }


    }
}
