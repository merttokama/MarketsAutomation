using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Regions")]
    public class Regions
    {
        [Key]
        public int SuppliersID { get; set; }
        [StringLength(20)]
        public string City { get; set; }
        [StringLength(20)]
        public string Country { get; set; }
        [StringLength(20)]
        public string Postal_Code { get; set; }
        [ForeignKey("SuppliersID")]
        public virtual Suppliers Suppliers { get; set; }

    }
}
