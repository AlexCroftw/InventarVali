using InventarVali.Models;

namespace InventarVali.DataAccess.Repository.IRepository
{
    public interface IAutovevehiculeInvoiceRepository : IRepository<AutovehiculeInvoice>
    {
        void Update(AutovehiculeInvoice obj);
    }
}
