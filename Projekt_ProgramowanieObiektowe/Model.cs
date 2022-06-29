using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projekt_ProgramowanieObiektowe
{
    public class TableContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Receipts> Receipts { get; set; }
        public DbSet<ProductsOnReceipt> ProductsOnReceipt { get; set; }
        public DbSet<Storage> Storage { get; set; }
        public string ConnectionString { get; }
        public TableContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(this.ConnectionString);
        }
    }
    public class Products
    {
        [Key]
        public int productID { get; set; }
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public decimal discount { get; set; }

        public List<ProductsOnReceipt> productsOnReceipts { get; } = new List<ProductsOnReceipt>();
        public Storage Storage { get; set; }
    }

    public class Receipts
    {
        [Key]
        public int receiptID { get; set; }
        public DateTime purchaseTime { get; set; }
        public List<ProductsOnReceipt> productsOnReceipts { get; } = new List<ProductsOnReceipt>();

    }

    public class ProductsOnReceipt
    {
        [Key]
        public int porID { get; set; }
        public int productID { get; set; }
        public Products product { get; set; }

        public int receiptID { get; set; }
        public Receipts receipt { get; set; }
        public decimal amount { get; set; }
        public decimal discount { get; set; }
        public decimal sumPrice { get; set; }
    }

    public class Storage
    {
        [Key]
        public int storageID { get; set; }
        public int productID { get; set; }
        public Products product { get; set; }
        public decimal amount { get; set; }
    }
}
