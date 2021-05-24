using FinalProjectStore.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectStore.Data.Entities
{
    public class SoldProduct : BaseEntity
    {
        private static int Count = 0;
        public Product Product { get; set; }

        public SoldProduct()
        {
            Count++;
            Number = Count;
        }
    }
}
