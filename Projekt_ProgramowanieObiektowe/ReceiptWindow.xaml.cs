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
    /// Logika interakcji dla klasy ReceiptWindow.xaml
    /// </summary>
    public partial class ReceiptWindow : Window
    {
        private List<ReceiptManager> productsOnReceipt = new List<ReceiptManager>();
        private int receiptID = 0;
        public ReceiptWindow()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(receiptIDBox.Text, out int result))
            {
                receiptID = result;
            } else
            {
                MessageBox.Show("Something went wrong!", "Error!");
                return;
            }

            productsOnReceipt =
                (from por in App.tc.ProductsOnReceipt
                 join receipt in App.tc.Receipts on por.receiptID equals receipt.receiptID
                 join product in App.tc.Products on por.productID equals product.productID
                 where por.receiptID == receiptID
                 select new ReceiptManager
                 {
                     por = por,
                     receipt = receipt,
                     product = product
                 }
                 ).ToList();

            receiptGrid.ItemsSource = productsOnReceipt;
            receiptGrid.Items.Refresh();

        }

        private void addNewReceipt_Click(object sender, RoutedEventArgs e)
        {
            NewReceiptWindow window = new NewReceiptWindow();
            window.ShowDialog();
        }
    }
}
