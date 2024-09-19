using InventarVali.Models;

namespace InventarVali.DataAccess.Repository.IRepository
{
    public interface IAutovehiculeRepository : IRepository<Autovehicule>
    {
        void Update(Autovehicule obj);
    }
}
