using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BLL.Repository;
using DevExpress.XtraLayout.Utils;
using System.IO;
using Entity.View;
using Entity.Models;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Collections;

namespace WFA.UI
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region METHODS

        #region GENERAL MOTHOD

        void KolonSifirla()
        {
            int maxColumnIndex = gridView1.Columns.Count - 1;

            for (int i = maxColumnIndex; i >= 0; i--)
            {
                gridView1.Columns.RemoveAt(i);
            }
        }
        void TablariKapa()
        {
            layoutProducts.Visibility = LayoutVisibility.Never;
            layoutSuppliers.Visibility = LayoutVisibility.Never;
            layoutSales.Visibility = LayoutVisibility.Never;
            layoutOrders.Visibility = LayoutVisibility.Never;
            layoutSubcategories.Visibility = LayoutVisibility.Never;
            layoutCategories.Visibility = LayoutVisibility.Never;
        }
        void GridSeciminiSıfırla()
        {
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        #endregion

        #region VIEW METHODS

        void ViewCategories()
        {
            gridControl1.DataSource = new CategoriesRepo().GetAll();
        }
        void ViewSubcategories()
        {
            gridControl1.DataSource = new Categories_DetailsRepo().GetAll();
        }
        void ViewOrders()
        {
            gridControl1.DataSource = new OrdersRepo().GetAll();
        }
        void ViewProducts()
        {
            gridControl1.DataSource = new ProductsRepo().GetAll();
        }
        void ViewSuppliers()
        {
            gridControl1.DataSource = new SuppliersRepo().GetAll().Select(x => new SuppliersView()
            {
                SuppliersID = x.SuppliersID,
                CompanyName = x.CompanyName,
                ContactName = x.ContactName
            });
        }
        void ViewSales()
        {
            gridControl1.DataSource = new SalesRepo().GetAll();
        }

        #endregion

        #endregion

        #region DEFINITIONS

        ArrayList lstProductId = new ArrayList();
        byte[] resimArray = new byte[64];
        MemoryStream memoryStream = new MemoryStream();

        #endregion

        #region BUTTON CLICK

        #region LEFT SIDE BUTTON CLICK

        private void btnCategories_Click(object sender, EventArgs e)
        {
            GridSeciminiSıfırla();
            KolonSifirla();
            gridControl1.DataSource = new CategoriesRepo().GetAll();
            TablariKapa();
            layoutCategories.Visibility = LayoutVisibility.Always;
        }

        private void btnSubcategories_Click(object sender, EventArgs e)
        {
            GridSeciminiSıfırla();
            KolonSifirla();
            cmBoxCategory.Items.Clear();
            gridControl1.DataSource = new Categories_DetailsRepo().GetAll();
            List<Categories> lst = new CategoriesRepo().GetAll();
            foreach (Categories item in lst)
            {
                cmBoxCategory.Items.Add(item);
            }
            TablariKapa();
            layoutSubcategories.Visibility = LayoutVisibility.Always;
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            GridSeciminiSıfırla();
            KolonSifirla();
            cmBoxSubcategory.Items.Clear();
            gridControl1.DataSource = new ProductsRepo().GetAll();
            List<Categories_Details> lst = new Categories_DetailsRepo().GetAll();
            foreach (Categories_Details item in lst)
            {
                cmBoxSubcategory.Items.Add(item);
            }
            TablariKapa();
            layoutProducts.Visibility = LayoutVisibility.Always;
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            GridSeciminiSıfırla();
            KolonSifirla();
            gridControl1.DataSource = null;
            ViewSuppliers();
            TablariKapa();
            layoutSuppliers.Visibility = LayoutVisibility.Always;
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            GridSeciminiSıfırla();
            KolonSifirla();
            gridControl1.DataSource = new SalesRepo().GetAll();
            cmBoxPymentType.DataSource = Enum.GetValues(typeof(OdemeTipi));
            TablariKapa();
            layoutSales.Visibility = LayoutVisibility.Always;
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            cmBoxOrdersProducts.Items.Clear();
            cmBoxOrdersSuppliers.Items.Clear();
            GridSeciminiSıfırla();
            KolonSifirla();
            gridControl1.DataSource = new OrdersRepo().GetAll();
            List<Products> lst = new ProductsRepo().GetAll();
            foreach (Products item in lst)
            {
                cmBoxOrdersProducts.Items.Add(item);
            }
            List<Suppliers> lst1 = new SuppliersRepo().GetAll();
            foreach (Suppliers item in lst1)
            {
                cmBoxOrdersSuppliers.Items.Add(item);
            }
            TablariKapa();
            layoutOrders.Visibility = LayoutVisibility.Always;
        }

        #endregion

        #region RIGHT SIDE BUTTON CLICK

        private void btnExit_Click(object sender, EventArgs e)
        {
            UserGate frm = new UserGate();
            frm.Show();
            this.Close();
        }

        private void btnSaveDB_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabPage == layoutCategories)
            {
                try
                {
                    Categories cat =
                    new CategoriesRepo().Insert2(new Categories()
                    {
                        CategoryName = txtCategoryName.Text,
                        CategoryKDV = Convert.ToDouble(numCategoryKDVRate.Value / 100)

                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ViewCategories();
            }
            if (tabControl1.SelectedTabPage == layoutSubcategories)
            {
                try
                {
                    new Categories_DetailsRepo().Insert(new Categories_Details()
                    {
                        Category_DetailsName = txtSubcategoryName.Text,
                        Expiration_Date = Convert.ToInt32(numExpirationDate.Value),
                        CategoryID = Convert.ToInt32((cmBoxCategory.SelectedItem as Categories).CategoryID)
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ViewSubcategories();
            }
            if (tabControl1.SelectedTabPage == layoutProducts)
            {
                try
                {
                    Products pro =
                    new ProductsRepo().Insert2(new Products()
                    {
                        ProductName = txtProductName.Text,
                        IsItOnSale = checkBoxIsItOnSale.Checked
                    });

                    new Products_DetailsRepo().Insert(new Products_Details()
                    {
                        Category_DetailsID = pro.ProductID,
                        ProductID = pro.ProductID,
                        Barcode = txtProductBarcode.Text,
                        // Image = null,
                        SalesPrice = Convert.ToDecimal(txtProductSalesPrice.Text),
                        Stock = 0
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ViewProducts();
            }
            if (tabControl1.SelectedTabPage == layoutSuppliers)
            {
                try
                {
                    Suppliers supp =
                   new SuppliersRepo().Insert2(new Suppliers()
                   {
                       ContactName = txtContactName.Text,
                       CompanyName = txtCompanyName.Text
                   });

                    new Suppliers_DetailsRepo().Insert(new Suppliers_Details()
                    {
                        SuppliersID = supp.SuppliersID,
                        Phone = txtPhone.Text,
                        Address = txtAdress.Text,
                        EMail = txtEmail.Text
                    });

                    new RegionsRepo().Insert(new Regions()
                    {
                        SuppliersID = supp.SuppliersID,
                        City = txtCity.Text,
                        Country = txtCountry.Text,
                        Postal_Code = txtPostalCode.Text
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ViewSuppliers();
            }
            if (tabControl1.SelectedTabPage == layoutOrders)
            {
                int i = 0;
                try
                {
                    Orders order =
                    new OrdersRepo().Insert2(new Orders()
                    {
                        SuppliersID = Convert.ToInt32(cmBoxOrdersSuppliers.Text)
                    });
                    foreach (ListViewItem li in lstOrdersBaskets.Items)
                    {
                        new Orders_DetailsRepo().Insert(new Orders_Details()
                        {
                            OrderID = order.OrderID,
                            ProductID = Convert.ToInt32(lstProductId[i]),
                            Quantity = Convert.ToInt32(li.SubItems[1].Text),
                            BuyingPrice = Convert.ToDecimal(li.SubItems[2].Text)
                        });
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ViewOrders();
            }
            if (tabControl1.SelectedTabPage == layoutSales)
            {
                int i = 0;
                try
                {
                    Sales sales =
                    new SalesRepo().Insert2(new Sales()
                    {
                        Payment_Type = cmBoxPymentType.Text,
                        Discount = Convert.ToInt32(numSalesDiscount.Value) /// YANLIŞ TABLOYA YAZMIŞIM. O YÜZDEN ŞUANLIK SIKINTISI ÇÖZÜLEMİYOR.
                    });
                    foreach (ListViewItem li in lstSalesBasket.Items)
                    {
                        string a = li.SubItems[0].Text;
                        var productiDcekme = new ProductsRepo().GetAll().Where(x => x.ProductName == a)
                            .Select(x => x.ProductID);
                        int b = Convert.ToInt32(productiDcekme);

                        new Sales_DetailsRepo().Insert(new Sales_Details()
                        {
                            SalesID = sales.SalesID,
                            ProductID = b,
                            Quantity = Convert.ToInt32(li.SubItems[1].Text)
                        });
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ViewSales();
            }
        }

        private void btnUpdateDB_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRow() == null)
                return;
            if (tabControl1.SelectedTabPage == layoutCategories)
            {
                int categoryId = (gridView1.GetFocusedRow() as Categories).CategoryID;
                try
                {
                    var category = new CategoriesRepo().GetById(categoryId);
                    category.CategoryName = txtCategoryName.Text;
                    category.CategoryKDV = Convert.ToUInt32(numCategoryKDVRate.Value);
                    new CategoriesRepo().Update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ViewCategories();
            }
            if (tabControl1.SelectedTabPage == layoutSubcategories)
            {
                var subcategoryId = (gridView1.GetFocusedRow() as Categories_Details).Category_DetailsID;
                try
                {
                    var subcategory = new Categories_DetailsRepo().GetById(subcategoryId);
                    subcategory.Category_DetailsName = txtSubcategoryName.Text;
                    subcategory.Expiration_Date = Convert.ToInt32(numExpirationDate.Value);
                    subcategory.CategoryID = Convert.ToInt32(cmBoxCategory.Text);
                    new Categories_DetailsRepo().Update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ViewSubcategories();
            }
            if (tabControl1.SelectedTabPage == layoutProducts)
            {
                var productId = (gridView1.GetFocusedRow() as Products).ProductID;
                try
                {
                    var product = new ProductsRepo().GetById(productId);
                    product.ProductName = txtProductName.Text;
                    if (checkBoxIsItOnSale.Checked == true)
                        product.IsItOnSale = true;
                    else
                        product.IsItOnSale = false;
                    new ProductsRepo().Update();

                    var product2 = new Products_DetailsRepo().GetAll().Where(x => x.ProductID == productId);
                    foreach (Products_Details item in product2)
                    {
                        item.Barcode = txtProductBarcode.Text;
                        item.SalesPrice = Convert.ToDecimal(txtProductSalesPrice.Text);
                        item.Category_DetailsID = Convert.ToInt32(cmBoxSubcategory.Text);
                        //  item.Image = ImageToByteArray(picProduct.Image);
                    }
                    new Products_DetailsRepo().Update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ViewProducts();
            }
            if (tabControl1.SelectedTabPage == layoutSuppliers)
            {
                var suppliersId = (gridView1.GetFocusedRow() as SuppliersView).SuppliersID;
                try
                {
                    var suppliers = new SuppliersRepo().GetById(suppliersId);
                    suppliers.ContactName = txtContactName.Text;
                    suppliers.CompanyName = txtCompanyName.Text;
                    new SuppliersRepo().Update();

                    var suppliersdetails = new Suppliers_DetailsRepo().GetAll().Where(x => x.SuppliersID == suppliersId);
                    foreach (Suppliers_Details item in suppliersdetails)
                    {
                        item.Phone = txtPhone.Text;
                        item.Address = txtAdress.Text;
                        item.EMail = txtEmail.Text;
                    }
                    new Suppliers_DetailsRepo().Update();

                    var regions = new RegionsRepo().GetAll().Where(x => x.SuppliersID == suppliersId);
                    foreach (Regions item in regions)
                    {
                        item.City = txtCity.Text;
                        item.Country = txtCountry.Text;
                        item.Postal_Code = txtPostalCode.Text;
                    }
                    new RegionsRepo().Update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ViewSuppliers();
            }
            if (tabControl1.SelectedTabPage == layoutOrders)
            {
                int i = 0;
                var orderId = (gridView1.GetFocusedRow() as Orders).OrderID;
                try
                {
                    var orders = new OrdersRepo().GetById(orderId);
                    orders.SuppliersID = Convert.ToInt32(cmBoxOrdersSuppliers.Text);
                    new OrdersRepo().Update();

                    var ordersdetails = new Orders_DetailsRepo().GetAll().Where(x => x.OrderID == orderId);
                    foreach (ListViewItem li in lstOrdersBaskets.Items)
                    {
                        new Orders_DetailsRepo().Insert(new Orders_Details()
                        {
                            OrderID = orderId,
                            ProductID = Convert.ToInt32(lstProductId[i]),
                            Quantity = Convert.ToInt32(li.SubItems[1].Text),
                            BuyingPrice = Convert.ToDecimal(li.SubItems[2].Text)
                        });
                        i++;
                    }
                    new Orders_DetailsRepo().Update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ViewOrders();
            }
            if (tabControl1.SelectedTabPage == layoutSales)
            {
                int i = 0;
                var salesId = (gridView1.GetFocusedRow() as Sales).SalesID;
                try
                {
                    var sales = new SalesRepo().GetById(salesId);
                    sales.Discount = Convert.ToInt32(numSalesDiscount.Value);
                    sales.Payment_Type = cmBoxPymentType.Text;
                    new SalesRepo().Update();

                    var salesdetails = new Sales_DetailsRepo().GetAll().Where(x => x.SalesID == salesId);
                    foreach (ListViewItem li in lstSalesBasket.Items)
                    {
                        string a = li.SubItems[0].Text;
                        var productiDcekme = new ProductsRepo().GetAll().Where(x => x.ProductName == a)
                            .Select(x => x.ProductID);
                        int b = Convert.ToInt32(productiDcekme);

                        new Sales_DetailsRepo().Insert(new Sales_Details()
                        {
                            SalesID = salesId,
                            ProductID = b,
                            Quantity = Convert.ToInt32(li.SubItems[1].Text),
                        });
                        i++;
                    }
                    new Sales_DetailsRepo().Update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ViewSales();
            }
        }

        #endregion

        #region INSIDE TAB CLICK

        private void lstOrdersBaskets_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem li in lstOrdersBaskets.SelectedItems)
            {
                txtSalesBarcode.Text = li.SubItems[0].Text;
                numSalesQuantity.Value = Convert.ToInt32(li.SubItems[1].Text);
                txtBuyingPrice.Text = li.SubItems[2].Text;
            }
        }

        private void lstSalesBasket_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem li in lstSalesBasket.SelectedItems)
            {
                string a = li.SubItems[0].Text;
                var barcodeint = new Products_DetailsRepo().GetAll().Where(x => x.Products.ProductName == a)
               .Select(x => x.Barcode);
                txtSalesBarcode.Text = barcodeint.ToString();
                numSalesQuantity.Value = Convert.ToInt32(li.SubItems[1].Text);
                numSalesDiscount.Value = Convert.ToUInt32(li.SubItems[2].Text);
                cmBoxPymentType.Text = li.SubItems[3].Text;
            }
        }

        private void btnOrderRemove_Click(object sender, EventArgs e)
        {
            lstOrdersBaskets.Items.Remove(lstOrdersBaskets.SelectedItems[0]);
        }

        private void btnOrderClearBasket_Click(object sender, EventArgs e)
        {
            lstOrdersBaskets.Items.Clear();
        }

        private void btnSalesAddBasket_Click(object sender, EventArgs e)
        {
            var barcodesearc = txtSalesBarcode.Text;
            decimal indirim = Convert.ToDecimal(numSalesDiscount.Value);
            var probarcode = new Products_DetailsRepo().GetAll().Where(x => x.Barcode == barcodesearc)
                .Select(x => x.Products.ProductName);
            var salesprice = new Products_DetailsRepo().GetAll().Where(x => x.Barcode == barcodesearc)
                .Select(x => x.SalesPrice);
            decimal satisfiyat = Convert.ToDecimal(salesprice);
            decimal sonuc = satisfiyat - ((satisfiyat * indirim) / 100);
            ListViewItem li = new ListViewItem();
            li.Text = probarcode.ToString();
            li.SubItems.Add(numSalesQuantity.Value.ToString());
            li.SubItems.Add(sonuc.ToString());
            li.SubItems.Add(cmBoxPymentType.Text);
            lstOrdersBaskets.Items.Add(li);
        }

        private void btnSalesRemove_Click(object sender, EventArgs e)
        {
            lstSalesBasket.Items.Remove(lstOrdersBaskets.SelectedItems[0]);
        }

        private void btnSalesClearBasket_Click(object sender, EventArgs e)
        {
            lstSalesBasket.Items.Clear();
        }

        private void btnOrdersAdd_Click(object sender, EventArgs e)
        {
            Products selectedPro = cmBoxOrdersProducts.SelectedItem as Products;
            lstProductId.Add(selectedPro.ProductID);
            ListViewItem li = new ListViewItem();
            li.Text = cmBoxOrdersProducts.SelectedItem.ToString();
            li.SubItems.Add(numOrdersQuantity.Value.ToString());
            li.SubItems.Add(txtBuyingPrice.Text);
            lstOrdersBaskets.Items.Add(li);
        }

        private void picProduct_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog1 = new OpenFileDialog();
            if (dialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream dosya = File.Open(dialog1.FileName, FileMode.OpenOrCreate);
                while (dosya.Read(resimArray, 0, resimArray.Length) != 0)
                {
                    memoryStream.Write(resimArray, 0, resimArray.Length);
                }
                dosya.Close();
                dosya.Dispose();
                picProduct.Image = new Bitmap(memoryStream);
            }
        }

        #endregion

        #endregion

        #region FORM LOAD

        private void MainForm_Load(object sender, EventArgs e)
        {
            GridSeciminiSıfırla();
            ViewCategories();
            TablariKapa();
            layoutCategories.Visibility = LayoutVisibility.Always;
        }

        #endregion

        #region GRIDVIEW ROW CLICK

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridView1.GetSelectedRows().Length == 0)
                return;

            if (tabControl1.SelectedTabPage == layoutCategories)
            {
                var selectedRow = (gridView1.GetFocusedRow()) as Categories;
                txtCategoryName.Text = selectedRow.CategoryName;
                numCategoryKDVRate.Value = Convert.ToInt32(selectedRow.CategoryKDV);
            }

            if (tabControl1.SelectedTabPage == layoutSubcategories)
            {
                var selectedRow = (gridView1.GetFocusedRow()) as Categories_Details;
                cmBoxCategory.Text = selectedRow.CategoryID.ToString();
                numExpirationDate.Value = selectedRow.Expiration_Date;
                txtSubcategoryName.Text = selectedRow.Category_DetailsName;
            }
            if (tabControl1.SelectedTabPage == layoutProducts)
            {
                //foreach (var item in this.Controls)
                //{
                //    if (item is TextBox)
                //        ((TextBox)item).Clear();
                //}
                txtProductBarcode.Text = "";
                txtProductSalesPrice.Text = "";
                cmBoxSubcategory.Text = "";

                var selectedRow1 = (gridView1.GetFocusedRow()) as Products;
                List<Products_Details> productdetails = new Products_DetailsRepo().productIdileGetiren(selectedRow1.ProductID);
                txtProductName.Text = selectedRow1.ProductName;
                foreach (var item in productdetails)
                {
                    lblStock.Text = item.Stock.ToString();
                    txtProductBarcode.Text = item.Barcode;
                    txtProductSalesPrice.Text = (item.SalesPrice).ToString();
                    cmBoxSubcategory.Text = item.Category_DetailsID.ToString();
                }
                if (selectedRow1.IsItOnSale == true)
                    checkBoxIsItOnSale.Checked = true;
                else
                    checkBoxIsItOnSale.Checked = false;
                //   picProduct.Image = ImageToByteArray(selectedRow.Image);
            }
            if (tabControl1.SelectedTabPage == layoutSuppliers)
            {
                var selectedRow = (gridView1.GetFocusedRow()) as SuppliersView;
                List<Regions> regions = new RegionsRepo().regionIdileGetiren(selectedRow.SuppliersID);
                List<Suppliers_Details> suppliersdetails = new Suppliers_DetailsRepo().suppliersdetailsGetiren(selectedRow.SuppliersID);
                txtContactName.Text = selectedRow.ContactName;
                txtCompanyName.Text = selectedRow.CompanyName;
                foreach (var item in suppliersdetails)
                {
                    txtEmail.Text = item.EMail;
                    txtPhone.Text = item.Phone;
                    txtAdress.Text = item.Address;
                }
                foreach (var item in regions)
                {
                    txtCountry.Text = item.Country;
                    txtCity.Text = item.City;
                    txtPostalCode.Text = item.Postal_Code;
                }
            }
            if (tabControl1.SelectedTabPage == layoutOrders)
            {
                var selectedRow1 = gridView1.GetFocusedRow() as Orders;
                List<Orders_Details> orderdetails = new Orders_DetailsRepo().orderIdileGetiren(selectedRow1.OrderID);
                foreach (var item in orderdetails)
                {
                    ListViewItem li = new ListViewItem();
                    li.Text = item.Products.ProductName.ToString();
                    li.SubItems.Add(item.Quantity.ToString());
                    li.SubItems.Add(item.BuyingPrice.ToString());
                    lstOrdersBaskets.Items.Add(li);
                }
            }
            if (tabControl1.SelectedTabPage == layoutSales)
            {
                var selectedRow1 = (gridView1.GetFocusedRow()) as Sales;
                List<Sales_Details> salesdetails = new Sales_DetailsRepo().salesIdileGetiren(selectedRow1.SalesID);
                foreach (var item in salesdetails)
                {
                    ListViewItem li = new ListViewItem();
                    li.Text = item.Products.ProductName.ToString();
                    li.SubItems.Add(item.Quantity.ToString());
                    li.SubItems.Add(item.Sales.Discount.ToString());  //???????
                    li.SubItems.Add(item.Sales.Payment_Type.ToString());
                    lstOrdersBaskets.Items.Add(li);
                }
            }
        }





        #endregion

    }
}
