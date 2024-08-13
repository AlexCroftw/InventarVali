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
    public class GoodsRepository : Repository<Goods>, IGoodsRepository
    {
        private ApplicationDbContext _db;
        public GoodsRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Goods obj)
        {
           _db.Goods.Update(obj);
        }
    }
}
