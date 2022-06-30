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
    /// Class that holds values for product in basket
    /// </summary>
    public class BasketProduct
    {
        public Products product { get; set; }
        public string ProductName
        {
            get
            {
                return product.productName;
            }
        }
        public decimal Amount { get; set; }
        public decimal Discount
        {
            get
            {
                return product.discount;
            }
        }
        public decimal Price
        {
            get
            {
                return product.productPrice;
            }
        }

        public decimal SumPrice
        {
            get
            {
                return Amount * (Price - Discount);
            }
        }
    }
    /// <summary>
    /// Logika interakcji dla klasy NewReceiptWindow.xaml
    /// </summary>
    public partial class NewReceiptWindow : Window
    {
        public List<StorageProduct> storage =
                (from productInStorage in App.tc.Storage
                 join product in App.tc.Products on productInStorage.productID equals product.productID
                 select new StorageProduct
                 {
                     product = product,
                     storage = productInStorage

                 }).ToList();
        public List<BasketProduct> basket = new List<BasketProduct>();
        public NewReceiptWindow()
        {
            InitializeComponent();
            storageGrid.ItemsSource = storage;
            storageGrid.Items.Refresh();
        }

        /// <summary>
        /// Check if input is correct and then add product from storage to basket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (storageGrid.SelectedItem == null) return;
            decimal amount = -1;
            if (decimal.TryParse(amountBox.Text, out decimal result))
            {
                amount = result;
            } else
            {
                MessageBox.Show("Incorrect amount", "Error!");
                return;
            }
            StorageProduct selected = storageGrid.SelectedItem as StorageProduct;
            if(!CheckIfExists(selected.product, out BasketProduct existingItem)) {
                basket.Add(new BasketProduct { product = selected.product, Amount = amount });
            } else
            {
                existingItem.Amount += amount;
            }
            basketGrid.ItemsSource = basket;
            basketGrid.Items.Refresh();
            amountBox.Text = "1";
        }

        /// <summary>
        /// Function that checks if product exits in basket
        /// </summary>
        /// <param name="item"></param>
        /// <param name="returnedItem"></param>
        /// <returns></returns>
        private bool CheckIfExists(Products item, out BasketProduct returnedItem)
        {
            foreach (BasketProduct i in basket)
            {
                if (i.product == item)
                {
                    returnedItem = i;
                    return true;
                }
            }
            returnedItem = null;
            return false;
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
        /// Add every item from basket into new receipt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            Receipts r = new Receipts { purchaseTime = DateTime.Now};
            App.tc.Receipts.Add(r);
            App.tc.SaveChanges();
            foreach(BasketProduct i in basket)
            {
                App.tc.ProductsOnReceipt.Add(new ProductsOnReceipt
                {
                    productID = i.product.productID,
                    receiptID = r.receiptID,
                    amount = i.Amount,
                    discount = i.Discount,
                    sumPrice = i.SumPrice
                });
            }
            App.tc.SaveChanges();
            MessageBox.Show("Added new receipt at ID: " + r.receiptID.ToString());
            this.Close();
        }

        /// <summary>
        /// Delete item from basket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(basketGrid.SelectedItem != null)
            {
                basketGrid.Items.Remove(basketGrid.SelectedItem);
                basketGrid.SelectedIndex = -1;
                deleteButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Change button activity after selecting an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void basketSelectChange(object sender, SelectionChangedEventArgs e)
        {
            if(basketGrid.SelectedItem != null)
            {
                deleteButton.IsEnabled = true;
            } else
            {
                deleteButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Exit window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
