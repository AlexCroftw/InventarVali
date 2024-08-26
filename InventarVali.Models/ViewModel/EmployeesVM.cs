using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models.ViewModel
{
    public class EmployeesVM
    {
        public Employees Employees { get; set; }
        public IEnumerable<SelectListItem> EmployeesList { get; set; }
    }
}
