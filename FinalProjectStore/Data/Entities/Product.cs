using FinalProjectStore.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectStore.Data.Entities
{
    public enum Category
    {
      Baby,
      Beverages,
      Laundry,
      Canned,
      Bakery,
      Tabacco
    }
    public class Product : BaseEntity
    {
        private static int Count = 1000;
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }

        public Product()
        {
            Count++;
            Code = Count;
        }
        
        
    }
}
