using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Address { get; set; }

        // Admin de la sucursal

        [ForeignKey("Admin")]
        public int AdminId { get; set; }


        // relacion 1 - N tables, menu, orders etc
        public ICollection<Table> Tables { get; set; }
        public ICollection<Menu> Menus { get; set; }
        public ICollection<Order> Orders { get; set; }



    }
}
