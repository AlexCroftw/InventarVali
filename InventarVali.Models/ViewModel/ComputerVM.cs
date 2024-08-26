using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Models.ViewModel
{
    public class ComputerVM
    {
        public Computer Computer { get; set; }
        public IEnumerable<SelectListItem> ComputerList { get; set; }
    }
}
