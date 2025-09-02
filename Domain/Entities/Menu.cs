    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Relación con sucursal
        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        // Productos del menú
        public ICollection<Product> Products { get; set; }
    }
}