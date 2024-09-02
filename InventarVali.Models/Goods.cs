using InventarVali.Models.ViewModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventarVali.Models
{
    public class Goods 
    {
        [Key]
        public int Id { get; set; }
        public Autovehicule Autovehicule { get; set; }
        public Computer Computer { get; set; }
        public Employees Employees { get; set; }

    }
}
