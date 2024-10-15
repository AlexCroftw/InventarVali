using InventarVali.DataAccess.Data;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
