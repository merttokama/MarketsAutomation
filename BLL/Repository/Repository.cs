using Entity.Models;
using Entity.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class CategoriesRepo : RepositoryBase<Categories, int> { }
    public class Categories_DetailsRepo : RepositoryBase<Categories_Details, int> { }
    public class OrdersRepo : RepositoryBase<Orders, int> { }
    public class Orders_DetailsRepo : RepositoryBase<Orders_Details, int> {

        public List<Orders_Details> orderIdileGetiren(int orderId)
        {
            dbContext = new DAL.MyContext();
            return dbContext.SiparisDetay.Where(x => x.OrderID == orderId).ToList();
        }
        public List<Products> allProducts(int product)
        {
            dbContext = new DAL.MyContext();
            return dbContext.Urunler.ToList();
        }

    }
    public class ProductsRepo : RepositoryBase<Products, int> { }
    public class Products_DetailsRepo : RepositoryBase<Products_Details, int> {
        public List<Products_Details> productIdileGetiren(int productId)
        {
            dbContext = new DAL.MyContext();
            return dbContext.UrunDetay.Where(x => x.ProductID == productId).ToList();
        }
    }
    public class RegionsRepo : RepositoryBase<Regions, int> {
        public List<Regions> regionIdileGetiren(int supId)
        {
            dbContext = new DAL.MyContext();
            return dbContext.Bolgeler.Where(x => x.SuppliersID == supId).ToList();
        }
    }
    public class SalesRepo : RepositoryBase<Sales, int> { }
    public class Sales_DetailsRepo : RepositoryBase<Sales_Details, int> {
        public List<Sales_Details> salesIdileGetiren(int salesId)
        {
            dbContext = new DAL.MyContext();
            return dbContext.SatısDetay.Where(x => x.SalesID == salesId).ToList();
        }
    }
    public class SuppliersRepo : RepositoryBase<Suppliers, int> { }
    public class Suppliers_DetailsRepo : RepositoryBase<Suppliers_Details, int> {
        public List<Suppliers_Details> suppliersdetailsGetiren(int sup1Id)
        {
            dbContext = new DAL.MyContext();
            return dbContext.TedarikciDetay.Where(x => x.SuppliersID == sup1Id).ToList();
        }
    }
    public class UsersRepo : RepositoryBase<Users, int> { }

    public class SuppliersViewRepo : RepositoryBase<SuppliersView, int> { }
}
