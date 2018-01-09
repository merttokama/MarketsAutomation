using Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MyContext : DbContext
    {
        public MyContext()
            : base("name=MyCon")
        {

        }

        public DbSet<Orders> Siparisler { get; set; }
        public DbSet<Categories> Kategoriler { get; set; }
        public DbSet<Categories_Details> AltKategoriler { get; set; }
        public DbSet<Orders_Details> SiparisDetay { get; set; }
        public DbSet<Products> Urunler { get; set; }
        public DbSet<Products_Details> UrunDetay { get; set; }
        public DbSet<Regions> Bolgeler { get; set; }
        public DbSet<Sales> Satıslar { get; set; }
        public DbSet<Sales_Details> SatısDetay { get; set; }
        public DbSet<Suppliers> Tedarikciler { get; set; }
        public DbSet<Suppliers_Details> TedarikciDetay { get; set; }
        public DbSet<Users> Kullanıcı { get; set; }
    }
}
