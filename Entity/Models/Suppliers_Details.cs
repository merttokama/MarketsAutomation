using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Suppliers_Details")]
    public class Suppliers_Details
    {
        [Key]
        public int SuppliersID { get; set; }
        [Required]
        [StringLength(24)]
        public string Phone { get; set; }
        [StringLength(60)]
        public string Address { get; set; }
        [StringLength(60)]
        public string EMail { get; set; }

        [ForeignKey("SuppliersID")]
        public virtual Suppliers Suppliers { get; set; }
    }
}
