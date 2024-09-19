using InventarVali.Models;

namespace InventarVali.DataAccess.Repository.IRepository
{
    public interface IComputerRepository : IRepository<Computer>
    {
        void Update(Computer obj);
    }
}
