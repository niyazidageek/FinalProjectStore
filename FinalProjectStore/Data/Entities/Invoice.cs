using FinalProjectStore.Data.Common;
using System;
using System.Collections.Generic;

namespace FinalProjectStore.Data.Entities
{
    //Ashaghda "Satish class-i teqdim olunub. Satishin mebleghi, tarixi, statusu, nomresi ve "Satish Item"-lari var.
    public class Invoice : BaseEntity
    {
        private static int Count = 0;
        public List<SoldProduct> SoldProducts  { get; set; }
        public double Cost { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public Invoice()   
        {
            Status = "Exists";
            SoldProducts = new();
            Count++;
            Number = Count;
            Date = DateTime.Now;
        }

    }
}
