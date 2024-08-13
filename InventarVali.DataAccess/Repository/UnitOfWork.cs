using InventarVali.DataAccess.Data;
using InventarVali.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IGoodsRepository Goods { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Goods = new GoodsRepository(_db);
        }
        
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
