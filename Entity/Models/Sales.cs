using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    [Table("Sales")]
    public class Sales
    {
        [Key]
        public int SalesID { get; set; }
        public DateTime Sales_Date { get; set; } = DateTime.Now;
        public string Payment_Type { get; set; } //Enum
        public int Discount { get; set; } = 0;

        public virtual List<Sales_Details> Sales_Details { get; set; }
    }

    public enum OdemeTipi { Nakit, Kredi_Kartı }
}
