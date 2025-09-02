using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int TableId { get; set; }
        [ForeignKey("TableId")]
        public Table Table { get; set; }

        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        [Range(0, 9999999.99)]
        [Column(TypeName = "decimal(9, 2)")]
        public decimal TotalPrice { get; set; }

        // Precio en dólares (API de terceros)


        
    }
}
