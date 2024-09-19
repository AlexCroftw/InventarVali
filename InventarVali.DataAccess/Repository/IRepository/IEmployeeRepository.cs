using InventarVali.Models;

namespace InventarVali.DataAccess.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employees>
    {
        void Update(Employees obj);
    }
}
