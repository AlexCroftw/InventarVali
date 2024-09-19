using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventarVali.Models.ViewModel
{
    public class AutovehiculeDetailsVM
    {
        public AutovehiculeVM Autovehicule { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
    }
}
