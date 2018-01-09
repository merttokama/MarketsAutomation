using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Categories Details")]
    public class Categories_Details
    {
        [Key]
        public int Category_DetailsID { get; set; }
        [Required]
        [StringLength(20)]
        public string Category_DetailsName { get; set; }
        public int Expiration_Date { get; set; } // Raf Ömrü
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Categories Categories { get; set; }

        public virtual List<Products_Details> Products_Details { get; set; }

        public override string ToString()
        {
            return Category_DetailsName.ToString();
        }
    }
    
}
