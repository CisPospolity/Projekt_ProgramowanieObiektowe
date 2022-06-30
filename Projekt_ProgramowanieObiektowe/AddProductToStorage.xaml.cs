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
using System.Text.RegularExpressions;

namespace Projekt_ProgramowanieObiektowe
{
    /// <summary>
    /// Logika interakcji dla klasy AddProductToStorage.xaml
    /// </summary>
    public partial class AddProductToStorage : Window
    {
        public List<Products> products = new List<Products>();
        /// <summary>
        /// Function that makes query that selects products that are in Products table but not in Storage table
        /// </summary>
        private void RefreshProducts()
        {
            products = (from product in App.tc.Products
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

        /// <summary>
        /// After clicking button this function will check if input is correct, then add product to Storage table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Function that checks if decimal number is inputed into text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        /// <summary>
        /// Function that changes button activity after slection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
