using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Suppliers")]
    public class Suppliers
    {
        [Key]
        public int SuppliersID { get; set; }
        [StringLength(20)]
        public string ContactName { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        public virtual List<Regions> Regions { get; set; }

        public virtual List<Suppliers_Details> Suppliers_Details { get; set; }

        public override string ToString()
        {
            return CompanyName.ToString(); 
        }
    }
}
