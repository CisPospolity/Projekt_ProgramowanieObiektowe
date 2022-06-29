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
using System.ComponentModel;

namespace Projekt_ProgramowanieObiektowe
{
    /// <summary>
    /// Logika interakcji dla klasy Products.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        public List<Products> products = App.tc.Products.ToList();
        private Products selectedProduct = null;
        private bool wasChanged = false;
        public ProductsWindow()
        {
            InitializeComponent();
            productsGrid.ItemsSource = products;
            productsGrid.Items.Refresh();
        }

        private void Refresh(object sender, CancelEventArgs e)
        {
            productsGrid.ItemsSource = App.tc.Products.ToList();
            productsGrid.Items.Refresh();
        }

        private void AddProduct_Button_Click(object sender, RoutedEventArgs e)
        {
            AddProduct window = new AddProduct();
            window.Closing += Refresh;
            window.ShowDialog();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {

            productsGrid.ItemsSource = App.tc.Products.ToList();
            productsGrid.Items.Refresh();

        }

        private void ProductGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProduct = productsGrid.SelectedItem as Products;
            if (selectedProduct != null)
            {
                DeleteButton.IsEnabled = true;
            }
            else
            {
                DeleteButton.IsEnabled = false;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            App.tc.Products.Remove(selectedProduct);
            App.tc.SaveChanges();
            selectedProduct = null;
            DeleteButton.IsEnabled = false;

            productsGrid.ItemsSource = App.tc.Products.ToList();
            productsGrid.Items.Refresh();
        }

        private void EditedRow(object sender, DataGridRowEditEndingEventArgs e)
        {
            wasChanged = true;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if(wasChanged)
            {
                App.tc.SaveChanges();
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
