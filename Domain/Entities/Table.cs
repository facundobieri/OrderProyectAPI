using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int TableNumber { get; set; }
        [Required]
        public bool IsAvailable { get; set; } = true;
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
