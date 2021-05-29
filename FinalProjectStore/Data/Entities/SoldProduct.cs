using FinalProjectStore.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FinalProjectStore.Data.Entities
{
    public class SoldProduct : BaseEntity
    {      
        private static int Count = 0;
        public Product Product { get; set; }
        public int quantity { get; set; }
        public SoldProduct(Product product)
        {
            Product = product;
            Count++;
            Number = Count;
        }
    }
}
