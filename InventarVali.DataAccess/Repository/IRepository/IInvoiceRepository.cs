using InventarVali.Models;

namespace InventarVali.DataAccess.Repository.IRepository
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        void Update(Invoice obj);
    }
}
