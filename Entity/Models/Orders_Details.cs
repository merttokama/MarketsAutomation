using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Orders_Details")]
    public class Orders_Details
    {
        [Key]
        [Column(Order = 1)]
        public int OrderID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal BuyingPrice { get; set; }

        [ForeignKey("OrderID")]
        public virtual Orders Orders { get; set; }
        [ForeignKey("ProductID")]
        public virtual Products Products { get; set; }
    }
}
