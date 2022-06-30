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

        /// <summary>
        /// Refresh items in ItemGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh(object sender, CancelEventArgs e)
        {
            productsGrid.ItemsSource = App.tc.Products.ToList();
            productsGrid.Items.Refresh();
        }

        /// <summary>
        /// Open Window for adding new product and refresh items after closing it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct_Button_Click(object sender, RoutedEventArgs e)
        {
            AddProduct window = new AddProduct();
            window.Closing += Refresh;
            window.ShowDialog();
        }

        /// <summary>
        /// Refresh items after clicking button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            productsGrid.Items.Refresh();
        }

        /// <summary>
        /// Change button activity after selecting an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Delete product from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Save changes in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if(wasChanged)
            {
                App.tc.SaveChanges();
            }
        }

        /// <summary>
        /// Close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
