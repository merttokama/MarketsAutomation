using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Sales_Details")]
    public class Sales_Details
    {
        [Key]
        [Column(Order = 1)]
        public int SalesID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }

        [ForeignKey("ProductID")]
        public virtual Products Products { get; set; }

        [ForeignKey("SalesID")]
        public virtual Sales Sales { get; set; }
    }
}
