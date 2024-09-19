namespace InventarVali.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        IAutovehiculeRepository Autovehicule { get; }
        IComputerRepository Computer { get; }
        void Save();
    }
}
