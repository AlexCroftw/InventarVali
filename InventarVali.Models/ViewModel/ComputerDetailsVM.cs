using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventarVali.Models.ViewModel
{
    public class ComputerDetailsVM
    {
        public ComputerVM Computer { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
    }
}
