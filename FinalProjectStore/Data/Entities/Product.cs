using FinalProjectStore.Data.Common;

namespace FinalProjectStore.Data.Entities
{
    //Ashaghda Enum teqdim olunub. "Category" enum-i bizim maghazamizdaki umumi kateqoriyalarini saxlayir.
    public enum Category
    {
      Baby,
      Beverages,
      Laundry,
      Canned,
      Bakery,
      Tabacco
    }
    //Ashaghda mehsul class-i teqdim olunub. Mehsul class-in adi, qiymeti, kategoriyasi(string kimi), sayi ve unikal 4 reqemli kodu var(hansiki ozu yaranir)
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
