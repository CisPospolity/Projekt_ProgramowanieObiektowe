using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_ProgramowanieObiektowe
{
    public class StorageProduct
    {
        public int id
        {
            get
            {
                return product.productID;
            }
        }

        public Products product { get; set; }
        public Storage storage { get; set; }

        public string Name
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
                return storage.amount;
            }
            set
            {
                storage.amount = value;
            }
        }
    }
}
