using InventarVali.DataAccess.Data;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;

namespace InventarVali.DataAccess.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        private ApplicationDbContext _db;

        public InvoiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Invoice obj)
        {
            _db.Invoice.Update(obj);
        }
    }
}
