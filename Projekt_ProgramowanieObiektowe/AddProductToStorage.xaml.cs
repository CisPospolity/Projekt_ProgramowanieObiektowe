using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekt_ProgramowanieObiektowe
{
    /// <summary>
    /// Logika interakcji dla klasy AddProductToStorage.xaml
    /// </summary>
    public partial class AddProductToStorage : Window
    {
        public List<Products> products =
            (from product in App.tc.Products
             join productInStorage in App.tc.Storage on product.productID equals productInStorage.productID
             into prod
             from storageProd in prod.DefaultIfEmpty()
             select new Products
             {
                 productID = product.productID,
                 productName = product.productName,
                 productPrice = product.productPrice
             }).Except(
                from product in App.tc.Products
                join productInStorage in App.tc.Storage on product.productID equals productInStorage.productID
                select new Products
                {
                    productID = product.productID,
                    productName = product.productName,
                    productPrice = product.productPrice
                }
             ).ToList();
        public AddProductToStorage()
        {
            InitializeComponent();
            productGrid.ItemsSource = products;
        }
    }
}
