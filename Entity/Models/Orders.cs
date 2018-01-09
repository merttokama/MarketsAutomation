using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public int SuppliersID { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime RequiredDate { get; set; } = DateTime.Now.AddDays(3);


        public virtual List<Orders_Details> Orders_Details { get; set; }
        [ForeignKey("SuppliersID")]
        [Display(AutoGenerateField =false)]
        public virtual Suppliers Suppliers { get; set; }


    }
}