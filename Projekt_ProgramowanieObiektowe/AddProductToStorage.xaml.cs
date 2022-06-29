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
        public List<Products> products = new List<Products>();
           private void RefreshProducts()
        {
            products= (from product in App.tc.Products
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
            productGrid.ItemsSource = products;
        }
        public AddProductToStorage()
        {
            InitializeComponent();
            RefreshProducts();
        }

        private void toStorageButton_Click(object sender, RoutedEventArgs e)
        {
            if(productGrid.SelectedItem != null)
            {
                decimal amoutToAdd = -1;
                if(decimal.TryParse(amountTextBox.Text,out decimal result))
                {
                    amoutToAdd = result;
                } else
                {
                    MessageBox.Show("Incorrect amount", "Error!");
                    return;
                }
                Products selectedProduct = productGrid.SelectedItem as Products;
                App.tc.Storage.Add(new Storage
                {
                    productID = selectedProduct.productID,
                    amount = amoutToAdd
                });
                App.tc.SaveChanges();
                RefreshProducts();
                productGrid.SelectedIndex = -1;
            }
        }

        private void GridSelectionChanges(object sender, SelectionChangedEventArgs e)
        {
            if(productGrid.SelectedItem != null)
            {
                toStorageButton.IsEnabled = true;
            } else
            {
                toStorageButton.IsEnabled = false;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
