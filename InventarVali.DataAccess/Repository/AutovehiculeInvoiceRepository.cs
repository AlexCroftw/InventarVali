using InventarVali.DataAccess.Data;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.DataAccess.Repository
{
    public class AutovehiculeInvoiceRepository : Repository<AutovehiculeInvoice>,IAutovevehiculeInvoiceRepository
    {
        private ApplicationDbContext _db;

        public AutovehiculeInvoiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(AutovehiculeInvoice obj)
        {
            _db.AutovehiculeInvoice.Update(obj);
        }
    }
}
