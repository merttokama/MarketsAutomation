using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Categories")]
    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        [StringLength(20)]
        public string CategoryName { get; set; }

        public double CategoryKDV { get; set; }

        public virtual List<Categories_Details> Categories_Details { get; set; } = new List<Categories_Details>();

        public override string ToString()
        {
            return CategoryName;
        }
    }
}
