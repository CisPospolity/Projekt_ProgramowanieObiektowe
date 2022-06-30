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
    /// Logika interakcji dla klasy AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
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
        /// After clicking button this function will check if input is correct, then add product to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            string name = ProductName.Text;
            decimal price = -1;

            if (decimal.TryParse(ProductPrice.Text.Replace('.',','), out decimal priceResult))
            {
                price = priceResult;
            }
            else
            {
                MessageBox.Show("Incorrect price format", "Error!");
                return;
            }

            if(price <=0)
            {
                MessageBox.Show("Price can't be lower than zero.", "Error!");
                return;
            }

            App.tc.Products.Add(new Products { productName = name, productPrice = price });
            App.tc.SaveChanges();
            MessageBox.Show(String.Format("{0} added at price {1}", name, price));
            this.Close();
        }

        /// <summary>
        /// Function that closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
