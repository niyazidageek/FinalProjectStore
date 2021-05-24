using FinalProjectStore.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectStore.Data.Entities
{
    public class Invoice : BaseEntity
    {
        private static int Count = 0;
        public SoldProduct SoldProduct { get; set; }
        public double Cost { get; set; }
        public DateTime Date { get; set; }

        public Invoice()
        {
            Count++;
            Number = Count;
        }

    }
}
