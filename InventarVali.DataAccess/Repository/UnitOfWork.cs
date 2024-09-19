using InventarVali.DataAccess.Data;
using InventarVali.DataAccess.Repository.IRepository;

namespace InventarVali.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IEmployeeRepository Employee { get; private set; }
        public IAutovehiculeRepository Autovehicule { get; private set; }
        public IComputerRepository Computer { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Employee = new EmployeeRepository(_db);
            Autovehicule = new AutovehiculeRepository(_db);
            Computer = new ComputerRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
