using FinalProjectStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProjectStore.Services
{  
    public class MarketMenu : IMarketable
    {
        public List<Product> Products { get; set; }
        public List<Invoice> Invoices { get; set; }
        public MarketMenu()
        {
            Products = new();
            Invoices = new();   
        }

        #region Product
        public void AddProduct(string name, double price, string category, int quantity)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Product name");
            //
            //Ashaghdaki kod, hemin ad-da mehsulun olub olmamasini yoxlayir. Eger hemin ad-da mehsul varsa, bize xeberdarliq gelecek. Proekta butov string inputlar case INSENSITIVE-dir.
            //
            int checkname = Products.FindIndex(s=> s.Name.ToLower() == name);
            if (checkname == 0)
                throw new Exception("This product already exists");
            if (price <= 0)
                throw new ArgumentOutOfRangeException("Product price");
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException("Product category");
            //
            //Ashaghdaki kod, gelen kategoriya adini artig maghazada olan kategoriyalari ile yoxlayir. Yoxlamanin neticesi pozitiv olsa, hemen mehsula verilmish kategoriya menimsedilir.
            //
            if (category.ToLower() != Category.Baby.ToString().ToLower() && category.ToLower() != Category.Bakery.ToString().ToLower() && category.ToLower() != Category.Beverages.ToString().ToLower() && category.ToLower() != Category.Canned.ToString().ToLower() && category.ToLower() != Category.Laundry.ToString().ToLower() && category.ToLower() != Category.Tabacco.ToString().ToLower())
                throw new KeyNotFoundException("There is no such category");
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("Product quantity");
            //
            //Ashaghdaki kod, teze mehsul yaradir.
            //
            Product product = new();
            product.Name = name; 
            product.Price = price;           
            product.Quantity = quantity;
            product.Category = category.ToLower();
            Products.Add(product);

        }
        public void ChangeProductByCode(int code, string name, double price, int quantity, string category)
        {
            Product product = Products.Find(s => s.Code == code);
            if (product == null)
                throw new KeyNotFoundException("There are no products with the given code");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Product name");
            if (code <= 1000)
                throw new ArgumentOutOfRangeException("Product code");
            if (price <= 0)
                throw new ArgumentOutOfRangeException("Product price");
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("Product quantity");
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException("Product category");
            //
            //Ashaghdaki kod, gelen kategoriya adini artig maghazada olan kategoriyalari ile yoxlayir.
            //
            if (category.ToLower() != Category.Baby.ToString().ToLower() && category.ToLower() != Category.Bakery.ToString().ToLower() && category.ToLower() != Category.Beverages.ToString().ToLower() && category.ToLower() != Category.Canned.ToString().ToLower() && category.ToLower() != Category.Tabacco.ToString().ToLower() && category.ToLower() != Category.Laundry.ToString().ToLower())
                throw new KeyNotFoundException("There is no such category");
            //
            //Ashaghdaki kod, deyishikleri apply edir.
            //
            product.Name = name;
            product.Price = price;       
            product.Quantity = quantity;
            product.Category = category.ToLower();
        }
        public void DeleteProductByCode(int code)
        {
            //Bu metod, verilmish koda gore mehsulu silir.
            if (code <= 1000)
                throw new ArgumentOutOfRangeException("Product code");
            Product product = Products.Find(s => s.Code == code);
            if (product == null)
                throw new KeyNotFoundException("There are no products with the given code");
            Products.Remove(product);
        }
        public List<Product> ReturnProducts()
        {
            return Products.ToList();
        }
        public List<Product> SearchProductByPrice(double startprice, double endprice)
        {
            //Ashaghdaki kod, mehsullarin qiymet aralighina gore axtarir
            if (startprice <= 0)
                throw new ArgumentOutOfRangeException("Product start price");
            if (endprice <= 0)
                throw new ArgumentOutOfRangeException("Product end price");
            var res = Products.FindAll(s => s.Price >= startprice && s.Price <= endprice);
            if (res == null)
                throw new KeyNotFoundException("There is no product within the given price range");
            return res.ToList();
        }
        public List<Product> SearchByCategory(string category)
        {
            //Bu metod verilmish kategoriyaya gore mehsullari gorsedir.
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException("Product category");
            if (category.ToLower() != Category.Baby.ToString().ToLower() && category.ToLower() != Category.Bakery.ToString().ToLower() && category.ToLower() != Category.Beverages.ToString().ToLower() && category.ToLower() != Category.Canned.ToString().ToLower() && category.ToLower() != Category.Tabacco.ToString().ToLower() && category.ToLower() != Category.Laundry.ToString().ToLower())
                throw new KeyNotFoundException("There is no such category");
            var res = Products.FindAll(s => s.Category.ToLower() == category.ToLower());
            if (res == null)
                throw new KeyNotFoundException("There is no product in the given category");
            return res.ToList();
        }
        public List<Product> SearchByName(string name)
        {
            //Bu metod, mehsullari ada gore axtarir.
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Product name");
            var temp = Products.FindAll(s => s.Name.ToLower() == name);
            if (temp == null)
                throw new KeyNotFoundException("There are no products with the given name"); 
            return temp.ToList();
        }
        #endregion

        #region Invoice
        public void AddInvoice(int code, int quantity)
        {
            int option = 0;
            if (code <= 1000)
                throw new ArgumentOutOfRangeException("Product code");
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("Product quantity");         
            Product product = Products.FirstOrDefault(s => s.Code == code);
            if (product == null)
                throw new KeyNotFoundException("There are no products with the given code");
            //
            //Eger mehsulun sayi 0 olsa ve biz o mehsulu almaq istesek, kod bize xeberdarliq edecek. Onnan sonra bize yeniden kodlarin siyahini ve sechim etmek shansini verecek.
            //
            while (product.Quantity == 0)
            {
                Console.WriteLine("The product is out of stock.");
                Console.WriteLine("Existing product codes are shown below:");
                foreach (var item in Products)
                {
                   Console.WriteLine($"code - {item.Code} ({item.Name})");
                }
                Console.WriteLine("Enter the code please");
                code = int.Parse(Console.ReadLine());
                if (code <= 1000)
                     throw new ArgumentOutOfRangeException("Product code");
                Console.WriteLine("Enter the quantity please");
                quantity = int.Parse(Console.ReadLine());
                if (quantity <= 0)
                     throw new ArgumentOutOfRangeException("Product quantity");
                product = Products.FirstOrDefault(s => s.Code == code);          
            }
            //
            //Eger biz verdiyimiz say mehsulun stokda sayinnan chox olarsa, kod bize xeberdarliq edib ve yeniden sayi daxil etmek uchun shans verecek.
            //
            while (quantity > product.Quantity)
            {
                Console.WriteLine($"The given quantity exceeds the real quantity of the product. The quantity of the product is: {product.Quantity}");
                Console.WriteLine("Re-enter the quantity");
                quantity = int.Parse(Console.ReadLine());
            }
            //
            //Mehsul yoxlamalari kechdikden sonra, asahghda verildiyi kimi, satisha elave olunur
            //  
            Invoice invoice = new();
            SoldProduct sold = new(product);
            sold.quantity = quantity;
            product.Quantity -= quantity;
            invoice.Cost += product.Price * quantity;   
            invoice.SoldProducts.Add(sold);
            //
            //Ashaghdaki mexanizma bizim bashqa bir mehsulun satisha elave olunmasini sorushur. Sechim teqdim olunur(switch case), ve sechmelisiniz(1-yes,2-no)
            //
            do
            {
                Console.WriteLine("Do you want to buy another item?");
                Console.WriteLine("1.Yes");
                Console.WriteLine("2.No");
                Console.WriteLine("Select an option, please");
                string optionstr = Console.ReadLine();
                while(!int.TryParse(optionstr, out option))
                {
                    Console.WriteLine("Enter a number, please");
                    optionstr = Console.ReadLine();
                }
                switch (option)
                {
                    case 1:
                        //
                        //Bu case-da satisha bashqa bir mehsul elave olunur. Yuxardaki kimi mehsul yoxlamadan kechir. Bir de, user sehv melumat daxil etse, ona yeniden hemin melumati daxil etmek shansi yaranir. Ona gore ashaghda choxlu kod var.
                        //
                        int code1;
                        int quantity1;
                        Console.WriteLine("Existing product codes are shown below:");
                        foreach (var item in Products)
                        {
                            Console.WriteLine($"code - {item.Code} ({item.Name})");
                        }
                        Console.WriteLine("Enter the code of the product, please"); 
                        string code1str = Console.ReadLine();
                        while(!int.TryParse(code1str, out code1) || code1<=1000 )
                        {
                            Console.WriteLine("Insert the code again");
                            code1str = Console.ReadLine();
                        } 
                        Console.WriteLine("Enter the quantity of the product, please");
                        string quantity1str = Console.ReadLine();
                        while(!int.TryParse(quantity1str, out quantity1))
                        {
                            Console.WriteLine("Insert the quantity again");
                            quantity1str = Console.ReadLine();
                        }                       
                        if (quantity1 <= 0)
                            throw new ArgumentOutOfRangeException("Product quantity");
                        Product product1 = Products.FirstOrDefault(s => s.Code == code1);
                        if (product1 == null)
                            throw new ArgumentNullException("There is no product with such code");                     
                        if (quantity1 <= 0)
                            throw new ArgumentOutOfRangeException("Product quantity");                                                  
                        while (product1.Quantity == 0)
                        {
                            Console.WriteLine("The product is out of stock.");         
                            Console.WriteLine("Existing product codes are shown below:");
                             foreach (var item in Products)
                             {
                                Console.WriteLine($"code - {item.Code} ({item.Name})");
                             }
                             Console.WriteLine("Enter the code please");
                             code1 = int.Parse(Console.ReadLine());
                             if (code1 <= 1000)
                                 throw new ArgumentOutOfRangeException("Product code");
                             Console.WriteLine("Enter the quantity please");
                             quantity1 = int.Parse(Console.ReadLine());
                             if (quantity1 <= 0)
                                 throw new ArgumentOutOfRangeException("Product quantity");
                             product1 = Products.FirstOrDefault(s => s.Code == code1);
                            if (product1 == null)
                                throw new ArgumentNullException("There is no product with such code");

                        }
                        while (quantity1 > product1.Quantity)
                        {
                            Console.WriteLine($"The given quantity exceeds the real quantity of the product. The quantity of the product is: {product1.Quantity}");
                            Console.WriteLine("Re-enter the quantity");
                            quantity1 = int.Parse(Console.ReadLine());
                        }
                        //
                        //Mehsul yoxlamadan sonra satisha elave olunur
                        //
                        SoldProduct sold1 = new(product1);
                        sold1 = new(product1);
                        sold1.quantity = quantity1;
                        product1.Quantity -= quantity1;
                        invoice.Cost += product1.Price * quantity1;
                        invoice.SoldProducts.Add(sold1);                        
                        break;
                    case 2:
                        //
                        //Axirda biz bashqa bir mehsulu elave etmek istemeyende 2-ye basiriq(yani "no"), ve avtomatik olaraq, satish sistema elave olunur.
                        //
                        Console.WriteLine("Invoice added");
                        Invoices.Add(invoice);
                        break;
                    default:
                        Console.WriteLine("There is no such option");
                        break;
                }
            } while (option!=2);                     
        }
        public void ReturnProduct(int number, string name, int quantity)
        {
            int option = 0;
            if (number <= 0)
                throw new ArgumentOutOfRangeException("Invoice number");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Product name");
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("Product quantity");
            Product product = Products.FirstOrDefault(s => s.Name.ToLower() == name);
            if (product == null)
                throw new KeyNotFoundException("There are no products with the given name");
            Invoice invoice = Invoices.FirstOrDefault(s=> s.Number == number);
            if (invoice == null)
                throw new KeyNotFoundException("There are no invoices with the given number");           
            SoldProduct sold = invoice.SoldProducts.FirstOrDefault(s => s.Product.Name.ToLower() == name);
            //
            //Qaytarmagh istediyiviz mehsulun melumatlari yuxarda yoxlanilir. Ashaghda ise, ne sayda qaytarmaq istediyimizi yoxlayir. Eger biz aldiqimizdan chox qaytarmagh isteyirikse, bize xeberdarliq edir ve yeniden sayi daxil etmek shansini yaradir.
            //
            while (sold.quantity < quantity)
            {
                Console.WriteLine("You can not return more than you bought.");
                Console.WriteLine("Enter the quantity, please");
                quantity = int.Parse(Console.ReadLine());
                if (quantity <= 0)
                    throw new ArgumentOutOfRangeException("Product quantity");
            }             
            if (sold == null)
                throw new KeyNotFoundException("There are no products in the given invoice");         
            //
            //Ashagda mehsul qaytarilir(anbarda hemin mehsul qaytardighimiz miqdar geder choxalir, ve satishin mebleqi da azalir)
            //
            product.Quantity += quantity;
            invoice.Cost -= product.Price * quantity;
            sold.quantity -= quantity;
         //
         //Ashagdaki mexanizma bizden daha bir mehsulu qaytarmaghimizi sorushur. Eger qaytarmag isteyirikse, yuxardaki prosesslari yeniden yere yetiri ve mehsul elave olunur. User neyise sehv daxil etse, kod ona yeniden hemin melumati daxil etmek shansini yaradir, ona gore choxlu kod var ashaghda.
         //
            do
            {
                Console.WriteLine("Do you want to return another item?");
                Console.WriteLine("1.Yes");
                Console.WriteLine("2.No");
                Console.WriteLine("Select an option, please");
                string optionstr = Console.ReadLine();
                while (!int.TryParse(optionstr, out option))
                {
                    Console.WriteLine("Enter a number, please");
                    optionstr = Console.ReadLine();
                }
                switch (option)
                {
                    case 1:
                        int quantity1;
                        Console.WriteLine("Enter the name of the product, please");
                        string name1 = Console.ReadLine().ToLower();
                        if (string.IsNullOrEmpty(name1))
                            throw new ArgumentNullException("Product name");
                        Console.WriteLine("Enter the quantity of the product, please");     
                        string quantity1str = Console.ReadLine();
                        while(!int.TryParse(quantity1str, out quantity1))
                        {
                            Console.WriteLine("Insert the quantity again");
                            quantity1str = Console.ReadLine();
                        }
                        if (quantity1 <= 0)
                            throw new ArgumentOutOfRangeException("Product quantity");
                        Product product1 = Products.FirstOrDefault(s => s.Name.ToLower() == name1);
                        if (product1 == null)
                            throw new KeyNotFoundException("Thre are no products with the given name");
                        SoldProduct sold1 = invoice.SoldProducts.FirstOrDefault(s => s.Product.Name.ToLower() == name1);
                        if (sold1 == null)
                            throw new KeyNotFoundException("There are no products in the given invoice");
                        while (sold1.quantity < quantity1)
                        {
                            Console.WriteLine("You can not return more than you bought");
                            Console.WriteLine("Enter the quantity again, please");
                            quantity1 = int.Parse(Console.ReadLine());
                        }
                        //
                        //Yuxarda yoxlama ughurlu olsa, mehsul ashaghi gedib qaytarilir.
                        //
                        product1.Quantity += quantity1;
                        invoice.Cost -= product1.Price * quantity1;
                        sold1.quantity -= quantity1;
                        break;
                    case 2:                      
                        //
                        //Eger artiq hechne qaytarmagh istemirikse, prosess dayandirilir. Ve eger biz butov mehsullari qaytarsag ve artiq satishin mebleqi 0 olsa, avtomatik olaraq satish silinir(statusu "Deleted" olur)
                        //
                        if (invoice.Cost == 0.00)
                        {
                            invoice.Status = "Deleted";
                        }                         
                        break;
                    default:
                        Console.WriteLine("There is no such option");
                        break;
                }
            } while (option != 2);
        }
        public List<Invoice> ReturnInvoices()
        {
            //Bu metod sadece umumi satishlari gorsedir
            return Invoices.ToList();
        }       
        public void DeleteInvoice(int no)
        {
            if (no <= 0)
                throw new ArgumentOutOfRangeException("Invoice number");     
            Invoice invoice = Invoices.FirstOrDefault(s=> s.Number == no);
            if (invoice == null)
                throw new KeyNotFoundException("There is no invoice with the given number");
            var res = invoice.SoldProducts.ToList();
            if (res == null)
                throw new KeyNotFoundException("There are no products in the given invoice");
            //
            //Ashaghdaki mexanizma, satish silinennen sonra, butov "Satish Itemlari" qaytarir. Sonra ise satishin statusunu "Deleted" kimi gorsedir
            //
            foreach (var item in res)
            {
                item.Product.Quantity += item.quantity;
            }
            invoice.Status = "Deleted";   
        }
        public List<Invoice> SearchByDate(DateTime startdate, DateTime enddate)
        {
            //Ashaghdaki metod satishlari tarix aralighina gore axtarir ve neticeni List kimi verir
            var result = Invoices.Where(m => m.Date>= startdate && m.Date <= enddate);
            if (result == null)
                throw new KeyNotFoundException("There were no invoices during the given date range");
            return result.ToList();
        }
        public List<Invoice> SearchInvoiceByPrice(double startcost, double endcost)
        {
            //Bu metod end-user-dan iki dene mebleg isteyir(bashlabgich ve bitme mebelghi). Hemin mebleg aralighina esasen olan satishlari gorsedir. List qaytarir
            if (startcost <= 0)
                throw new ArgumentOutOfRangeException("Invocie start cost");
            if (endcost <= 0)
                throw new ArgumentOutOfRangeException("Invocie end cost");
            var result = Invoices.FindAll(s => s.Cost >= startcost && s.Cost <= endcost);
            if (result == null)
                throw new KeyNotFoundException("There are no invoices within the given cost range");
            return result.ToList();
        }
        public List<Invoice> SearchByNumber(int no)
        {
            //Bu metod satishin nomresine esasen hemin satish haqqinda informasiya teqdim edir.
            if (no <= 0)
                throw new ArgumentOutOfRangeException("Invoice number");
            var res = Invoices.FindAll(s => s.Number == no);
            if (res == null)
                throw new KeyNotFoundException("There are no invoices with the given number");
            return res;            
        }
        public List<Invoice> SearchByOnlyDate(DateTime date)
        {
            //Bu metod sadece bir tarixe gore satishi axtarir. Hemin tarixde(yani hemin gunde) heyata kechirilmish satishlari List kimi gorsedecey.
            var res = Invoices.Where(s => s.Date.Day == date.Day && s.Date.Month == date.Month && s.Date.Month == date.Month && s.Date.Year == date.Year);
            if (res == null)
                throw new KeyNotFoundException("There were no inoices on this day");
            return res.ToList();
        }
        #endregion
    }
}
