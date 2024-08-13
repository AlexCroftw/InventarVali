using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IGoodsRepository Goods { get; }
        void Save();
    }
}
