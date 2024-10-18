namespace InventarVali.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        IAutovehiculeRepository Autovehicule { get; }
        IComputerRepository Computer { get; }
        IInvoiceRepository Invoice { get; }
        IAutovevehiculeInvoiceRepository AutovehiculeInvoice { get; }
        void Save();
    }
}
