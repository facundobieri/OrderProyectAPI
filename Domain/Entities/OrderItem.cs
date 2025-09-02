using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        
        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
        
        [Column(TypeName = "decimal(9, 2)")]
        public decimal UnitPrice { get; set; }
        
        [Column(TypeName = "decimal(9, 2)")]
        public decimal Subtotal => UnitPrice * Quantity;
    }
}