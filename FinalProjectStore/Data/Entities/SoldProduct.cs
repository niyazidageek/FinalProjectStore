using FinalProjectStore.Data.Common;

namespace FinalProjectStore.Data.Entities
{
    //Ashaghda "Satish  Item" class-i teqdim olunub. "Satish Item"-im yaranmasi uchun, satilan mehsuldan instans alinir.
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
