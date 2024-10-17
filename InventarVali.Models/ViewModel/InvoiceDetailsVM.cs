using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace InventarVali.Models.ViewModel
{
    public class InvoiceDetailsVM
    {
        public InvoiceVM Invoice { get; set; }

        [ValidateNever]
        public List<AutovehiculeVM> AutovehiculeVMList { get; set; }
    }
}
