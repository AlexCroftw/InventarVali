using InventarVali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.DataAccess.Repository.IRepository
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        void Update(Invoice obj);
    }
}
