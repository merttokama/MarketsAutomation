using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Product Details")]
    public class Products_Details
    {
        [Key]
        [Column(Order = 1)]
        public int Category_DetailsID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ProductID { get; set; }
        [Required]
        [StringLength(14)]
        public string Barcode { get; set; }

        public byte[] Image { get; set; } = null;
        [Required]
        public decimal SalesPrice { get; set; }

        public int Stock { get; set; } = 0;

        [ForeignKey("Category_DetailsID")]
        public virtual Categories_Details Category_Details { get; set; }
        [ForeignKey("ProductID")]
        public virtual Products Products { get; set; }
    }
}
