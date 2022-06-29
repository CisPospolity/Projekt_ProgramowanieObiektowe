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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_ProgramowanieObiektowe
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ShowProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow window = new ProductsWindow();
            window.ShowDialog();
        }

        private void showStorage_Click(object sender, RoutedEventArgs e)
        {
            StorageWindow window = new StorageWindow();
            window.ShowDialog();
        }

        private void manageReceipts_Click(object sender, RoutedEventArgs e)
        {
            ReceiptWindow window = new ReceiptWindow();
            window.ShowDialog();
        }
    }
}
