using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public bool IsItOnSale { get; set; } = false;

        public virtual List<Products_Details> Products_Details { get; set; }

        public virtual List<Sales_Details> Sales_Details { get; set; } = new List<Sales_Details>();

        public virtual List<Orders_Details> Orders_Details { get; set; } = new List<Orders_Details>();

        public override string ToString()
        {
            return ProductName;
        }
    }
}
