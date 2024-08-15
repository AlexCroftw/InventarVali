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
    public class AutovehiculeRepository : Repository<Autovehicule>, IAutovehicule
    {
        private ApplicationDbContext _db;
        public AutovehiculeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Autovehicule obj)
        {
            _db.Autovehicule.Update(obj);
        }
    }
}
