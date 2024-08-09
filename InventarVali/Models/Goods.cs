using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventarVali.Models
{
    public class Goods
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Goods name")]
        public string Name { get; set; }
        public string Type { get; set; }
        [Required]
        public bool IsTaken { get; set; }

    }
}
