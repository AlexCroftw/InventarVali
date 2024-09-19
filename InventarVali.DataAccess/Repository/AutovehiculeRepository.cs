using InventarVali.DataAccess.Data;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;

namespace InventarVali.DataAccess.Repository
{
    public class AutovehiculeRepository : Repository<Autovehicule>, IAutovehiculeRepository
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
