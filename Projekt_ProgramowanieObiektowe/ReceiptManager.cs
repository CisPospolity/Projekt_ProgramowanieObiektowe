using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_ProgramowanieObiektowe
{
    class ReceiptManager
    {
        public Receipts receipt { get; set; }
        public Products product { get; set; }
        public ProductsOnReceipt por { get; set; }

        public DateTime PurchaseTime
        {
            get
            {
                return receipt.purchaseTime;
            }
        }

        public string ProductName
        {
            get
            {
                return product.productName;
            }
        }

        public decimal Amount
        {
            get
            {
                return por.amount;
            }
        } 

        public decimal Discount
        {
            get
            {
                return por.discount;
            }
        }

        public decimal SumPrice
        {
            get
            {
                return por.sumPrice;
            }
        }
    }
}
