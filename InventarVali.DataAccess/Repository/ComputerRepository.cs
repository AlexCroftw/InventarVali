using InventarVali.DataAccess.Data;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;

namespace InventarVali.DataAccess.Repository
{
    public class ComputerRepository : Repository<Computer>, IComputerRepository
    {
        private readonly ApplicationDbContext _db;
        public ComputerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Computer obj)
        {
            _db.Computers.Update(obj);
        }


    }
}
