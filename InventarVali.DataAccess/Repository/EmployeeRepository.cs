using InventarVali.DataAccess.Data;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;

namespace InventarVali.DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employees>, IEmployeeRepository
    {
        private ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Employees obj)
        {
            _db.Employees.Update(obj);
        }
    }
}
