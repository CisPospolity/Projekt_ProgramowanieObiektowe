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
    public class NewReceipt
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
        public List<NewReceipt> newReceipt = new List<NewReceipt>();
        public NewReceiptWindow()
        {
            InitializeComponent();
            storageGrid.ItemsSource = storage;
            storageGrid.Items.Refresh();
        }

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
            if(!CheckIfExists(selected.product, out NewReceipt existingItem)) {
                newReceipt.Add(new NewReceipt { product = selected.product, Amount = amount });
            } else
            {
                existingItem.Amount += amount;
            }
            basketGrid.ItemsSource = newReceipt;
            basketGrid.Items.Refresh();
            amountBox.Text = "1";
        }

        private bool CheckIfExists(Products item, out NewReceipt returnedItem)
        {
            foreach (NewReceipt i in newReceipt)
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

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            Receipts r = new Receipts { purchaseTime = DateTime.Now};
            App.tc.Receipts.Add(r);
            App.tc.SaveChanges();
            foreach(NewReceipt i in newReceipt)
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

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(basketGrid.SelectedItem != null)
            {
                basketGrid.Items.Remove(basketGrid.SelectedItem);
                basketGrid.SelectedIndex = -1;
                deleteButton.IsEnabled = false;
            }
        }

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

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
