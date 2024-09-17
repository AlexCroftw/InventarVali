using InventarVali.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarVali.Utility.Services
{
    public interface IMyEmailSender
    {
        void SendEmail(string email, string subject, string htmlMessage);
    }
}
