﻿using System;
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
using Microsoft.EntityFrameworkCore;

namespace Projekt_ProgramowanieObiektowe
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class StorageWindow : Window
    {
        public List<StorageProduct> products =
                (from productInStorage in App.tc.Storage
                 join product in App.tc.Products on productInStorage.productID equals product.productID
                 select new StorageProduct
                 {
                     product = product,
                     storage = productInStorage

                 }).ToList();
        public StorageWindow()
        {
            InitializeComponent();
            storageGrid.ItemsSource = products;
            storageGrid.Items.Refresh();
        }
        private void RefreshView()
        {
            products =
                (from productInStorage in App.tc.Storage
                 join product in App.tc.Products on productInStorage.productID equals product.productID
                 select new StorageProduct
                 {
                     product = product,
                     storage = productInStorage

                 }).ToList();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            App.tc.SaveChanges();
            RefreshView();
            storageGrid.ItemsSource = products;
            storageGrid.Items.Refresh();
        }

        private void AddToStorage_Click(object sender, RoutedEventArgs e)
        {
            AddProductToStorage window = new AddProductToStorage();
            window.ShowDialog();
        }
    }
}