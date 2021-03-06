using ConsoleTables;
using FinalProjectStore.Data.Entities;
using System;
using System.Linq;

namespace FinalProjectStore.Services
{
    public class MenuService
    {
        static MarketMenu market = new();

        #region Display Methods
        public static void DisplayProducts()
        {
            //Bu metod umumi satishlari table kimi gorsedir
            var table = new ConsoleTable("Code", "Name", "Category", "Price", "Quantity");
            foreach (var product in market.ReturnProducts())
            {
                table.AddRow(product.Code, product.Name.ToString().First().ToString().ToUpper() + product.Name.Substring(1), product.Category.ToString().First().ToString().ToUpper() + product.Category.Substring(1), product.Price.ToString("0.00"), product.Quantity);
            }
            table.Write();
            Console.WriteLine();
        }
        public static void DisplayProductsByCategory()
        {
            //Bu metod kategoriyaya gore axtarish edir ve eticeni table-a chixardir
            var table = new ConsoleTable("Name", "Code", "Category", "Price", "Quantity");
            //Ashagdaki mexanizma size var olan kategoriyalari gorsedir, ve siz kategoriyani sechib elnen daxil etmelisiniz. Input case-INSENSITIVE-di(yani isteyirsiz balaca ya da boyuk herifler ile daxil edin, ferqi yoxdu)
            Console.WriteLine("=============Existing categories are below:==============");
            var categories = Enum.GetValues(typeof(Category));
            foreach (var item in categories)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Insert the category, please");
            var category = Console.ReadLine();
            try
            {
                foreach (var item in market.SearchByCategory(category))
                    //Ashagdaki prosess mehsulun melumatlarini table-a doldurur, ve string melumatlarinin bash herifini boyuk edir.
                {
                    table.AddRow(item.Name.ToString().First().ToString().ToUpper() + item.Name.Substring(1), item.Code, item.Category.ToString().First().ToString().ToUpper() + item.Category.Substring(1), item.Price.ToString("0.00"), item.Quantity);
                }
                table.Write();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again, please.");
                Console.WriteLine(e.Message);
            }
        }
        public static void DisplayProductByPrice()
        {
            //Bu metod mehsullari qiymet aralighina gore table-a chixardir
            var table = new ConsoleTable("Name", "Code", "Category", "Price", "Quantity");
            Console.WriteLine("Insert the minimum price of the product, please");
            double startprice;
            string startpricestr = Console.ReadLine();
            while (!double.TryParse(startpricestr, out startprice))
            {
                Console.WriteLine("Insert the minimum price again");
                startpricestr = Console.ReadLine();
            }
            Console.WriteLine("Insert the maximum price of the product, please");
            double endprice;
            string endpricestr = Console.ReadLine();
            while (!double.TryParse(endpricestr, out endprice))
            {
                Console.WriteLine("Insert the maximum price again");
                endpricestr = Console.ReadLine();
            }
            try
            {
                foreach (var item in market.SearchProductByPrice(startprice, endprice))
                {
                    table.AddRow(item.Name.ToString().First().ToString().ToUpper() + item.Name.Substring(1), item.Code, item.Category.ToString().First().ToString().ToUpper() + item.Category.Substring(1), item.Price.ToString("0.00"), item.Quantity);
                }
                table.Write();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Please, try again.");
                Console.WriteLine(e.Message);
            }
        }
        public static void DisplayProductByName()
        {
            //Bu metod ada gore axtarish edir ve mehsullari table-a chixardir. Adi ozuvuz daxil etmelisiniz. Ve ad case-INSENSITIVE-di(yani isteyirsiz balacaynan isteyirsiz boyuknen daxil edin. Anbara giren melumat filter olunub lower-case ile girir.)
            var table = new ConsoleTable("Names", "Code", "Category", "Price", "Quantity");
            Console.WriteLine("Insert the name of the product, please");
            var name = Console.ReadLine().ToLower();
            try
            {
                foreach (var item in market.SearchByName(name))
                {
                    table.AddRow(item.Name.ToString().First().ToString().ToUpper() + item.Name.Substring(1), item.Code, item.Category.ToString().First().ToString().ToUpper() + item.Category.Substring(1), item.Price.ToString("0.00"), item.Quantity);
                }
                table.Write();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Please, try again");
                Console.WriteLine(e.Message);
            }
        }
        public static void DisplayInvoices()
        {
            //Bu metod umumi satishlari table-a chixardir.
            var table = new ConsoleTable("Number", "Cost", "Quantity of the product types", "Status", "Date");
            foreach (var item in market.ReturnInvoices())
            {
                table.AddRow(item.Number, item.Cost.ToString("0.00"), item.SoldProducts.Count, item.Status, item.Date);
            }
            table.Write();
            Console.WriteLine();
        }
        public static void DisplayInvoicesByDate()
        {
            //Bu metod tarix aralighina gore satishlari tapib table-a chixardir.
            DateTime startdate;
            DateTime enddate;
            var table = new ConsoleTable("Number", "Cost", "Quantity of the product types", "Date", "Status");
            Console.WriteLine("Insert the start date, please (mm.dd.yyyy)");
            string startdatestr = Console.ReadLine();
            while(!DateTime.TryParse(startdatestr, out startdate))
            {
                Console.WriteLine("Date format is not correct. Insert the date again (mm.dd.yyyy)");
                startdatestr = Console.ReadLine();
            }            
            Console.WriteLine("Insert the end date, please (mm.dd.yyyy)");
            string enddatestr = Console.ReadLine();
            while(!DateTime.TryParse(enddatestr, out enddate))
            {
                Console.WriteLine("Date format is not correct. Insert the date again (mm.dd.yyyy)");
                enddatestr = Console.ReadLine();
            }
            try
            {
                foreach (var item in market.SearchByDate(startdate, enddate))
                {
                    table.AddRow(item.Number, item.Cost.ToString("0.00"), item.SoldProducts.Count, item.Date, item.Status);
                }
                table.Write();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again, please");
                Console.WriteLine(e.Message);
            }
        }
        public static void DisplayInvoiceByCost()
        {
            //Bu metod satishin meblegh aralighina gore satishlari gorsedir ve table-a chixardir.
            var table = new ConsoleTable("Number", "Cost", "Quantity of the product types", "Date", "Status");
            Console.WriteLine("Insert the minimum cost, please");
            double startcost;
            string startcoststr = Console.ReadLine();
            while(!double.TryParse(startcoststr, out startcost))
            {
                Console.WriteLine("Insert the minimum cost again");
                startcoststr = Console.ReadLine();
            }
            Console.WriteLine("Insert the maximum cost, please");
            double endcost;
            string endcoststring = Console.ReadLine();
            while(!double.TryParse(endcoststring, out endcost))
            {
                Console.WriteLine("Insert the maximum cost again");
                endcoststring = Console.ReadLine();
            }
            try
            {
                foreach (var item in market.SearchInvoiceByPrice(startcost, endcost))
                {
                    table.AddRow(item.Number, item.Cost.ToString("0.00"), item.SoldProducts.Count, item.Date, item.Status);
                }

                table.Write();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Please, try again");
                Console.WriteLine(e.Message);
            }
        }
        public static void DisplayInvoiceByNo()
        {
            //Bu metod 2 dene table qaytarir. Birinci table-da satish barede melumat gorsedilir. Ikinci satishda satilan mehsullar haqqinda melumat verilir.
            //
            //Birinci table ashaghdadi
            //
            var table = new ConsoleTable("Number", "Cost", "Date", "Status");
            Console.WriteLine("Existing invoice numbers are shown below:");
            foreach (var item in market.Invoices)
            {
                Console.WriteLine($"number - {item.Number}");
            }
            Console.WriteLine("Insert the number of the invoice, please");
            int no;
            string nostr = Console.ReadLine();
            while(!int.TryParse(nostr, out no))
            {
                Console.WriteLine("Insert the number again");
                nostr = Console.ReadLine();
            }
            try
            {
                foreach (var item in market.SearchByNumber(no))
                {
                    table.AddRow(item.Number, item.Cost.ToString("0.00"), item.Date, item.Status);
                }
                table.Write();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Please, try again");
                Console.WriteLine(e.Message);
            }
            //
            //Ikinci table ashaghdadi
            //
            var table1 = new ConsoleTable("Name", "Price", "Quantity", "Number");
            var res = market.Invoices.Find(s=>s.Number == no);   
            try
            {
                foreach (var invoice in res.SoldProducts)
                {
                    table1.AddRow(invoice.Product.Name.ToString().First().ToString().ToUpper() + invoice.Product.Name.Substring(1), invoice.Product.Price.ToString("0.00"), invoice.quantity, invoice.Number);
                }
                table1.Write();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Please, try again");
                Console.WriteLine(e.Message);
            }
        }
        public static void DisplayInvoiceByOnlyDate()
        {
            //Bu metod deqiq tarixe gore satishlari table-a chixardir.
            DateTime date;
            var table = new ConsoleTable("Number", "Cost", "Quantity of the product types", "Date", "Status");
            Console.WriteLine("Insert the date of the invoice, please (mm.dd.yyyy)");
            string datestr = Console.ReadLine();
            while(!DateTime.TryParse(datestr, out date))
            {
                Console.WriteLine("Date format is not correct. Insert the date again (mm.dd.yyyy)");
                datestr = Console.ReadLine();
            }
            try
            {                
                foreach (var item in market.SearchByOnlyDate(date))
                {
                    table.AddRow(item.Number, item.Cost.ToString("0.00"), item.SoldProducts.Count, item.Date, item.Status);
                }
                table.Write();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again, please");
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Addition/Removal Methods
        public static void AddProductMenu()
        {
            //Bu metod qarshi terefden mehsul haqqda melumat goturur ve variable-lara onu menimsedir.
            //Kategoriyani daxil etmek istesez size var olan kategoriyalari avtomatik olarag teqdim edir.
            Console.WriteLine("Enter the name of the product, please");
            string name = Console.ReadLine().ToLower();
            Console.WriteLine("Enter the price of the product, please");
            double price;
            string pricestr = Console.ReadLine();
            while(!double.TryParse(pricestr, out price))
            {
                Console.WriteLine("Please, insert the price again");
                pricestr = Console.ReadLine();
            } 
            Console.WriteLine("=============Existing categories are below:==============");
            var categories = Enum.GetValues(typeof(Category));
            foreach (var item in categories)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Enter the name of the product's category, please");
            string category = Console.ReadLine();
            Console.WriteLine("Enter the quantity of the product, please");
            int quantity;
            string quantitystr = Console.ReadLine();
            while(!int.TryParse(quantitystr, out quantity))
            {
                Console.WriteLine("Please, insert the quantity again");
                quantitystr = Console.ReadLine();
            }
            try
            {
                market.AddProduct(name, price, category, quantity);
                Console.WriteLine("=============Product added==============");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again, please");
                Console.WriteLine(e.Message);
            }  
        }
        public static void AddChangeProductMenu()
        {
            //Bu metod qarshi terefden evvel kod, sonra ise duzelish melumatlarini alir. Ilk once maghaza olan mehsullarin kodlarini ve yaninda adlarini gorsedir ki, siz chashmayasiz.
            Console.WriteLine("==============Existing product codes are shown below:==============");
            foreach (var item in market.Products)
            {
                Console.WriteLine($"code - {item.Code} ({item.Name})");
            }
            Console.WriteLine("Enter the code of the product, please");
            int code;
            string codestr = Console.ReadLine();
            while(!int.TryParse(codestr, out code))
            {
                Console.WriteLine("Insert the code again");
                codestr = Console.ReadLine();
            }
            Console.WriteLine("Enter the new name of the product, please");
            string name = Console.ReadLine().ToLower();
            Console.WriteLine("Enter the new price of the product, please");
            double price;
            string pricestr = Console.ReadLine();
            while(!double.TryParse(pricestr, out price))
            {
                Console.WriteLine("Insert the price again");
                pricestr = Console.ReadLine();
            }
            Console.WriteLine("Enter the new quantity of the product, please");
            int quantity;
            string quantitystr = Console.ReadLine();
            while(!int.TryParse(quantitystr, out quantity))
            {
                Console.WriteLine("Insert the quantity again");
                quantitystr = Console.ReadLine();
            }
            Console.WriteLine("=============Existing categories are below:==============");
            var categories = Enum.GetValues(typeof(Category));
            foreach (var item in categories)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Enter the new category of the product, please");
            string category = Console.ReadLine();
            try
            {
                market.ChangeProductByCode(code, name, price, quantity, category);
                Console.WriteLine("==============Product changed==============");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again, please");
                Console.WriteLine(e.Message);
            }   
        }
        public static void AddRemoveProductMenu()
        {
            //Bu metod qarshi terefden mehsulun kodu isteyir ve hemin mehsulu silir
            Console.WriteLine("==============Existing product codes are shown below:==============");
            foreach (var item in market.Products)
            {
                Console.WriteLine($"code - {item.Code} ({item.Name})");
            }
            Console.WriteLine("Enter the code of the product, please");
            int code;
            string codestr = Console.ReadLine();
            while (!int.TryParse(codestr, out code))
            {
                Console.WriteLine("Insert the code again");
                codestr = Console.ReadLine();
            }
            try
            {
                market.DeleteProductByCode(code);
                Console.WriteLine("==============Product deleted==============");
            }
            catch (Exception e)
            {
               Console.WriteLine("Something went wrong. Try again, please");
               Console.WriteLine(e.Message);
            }    
        }
        public static void AddInvoiceMenu()
        {
            //Bu metod qarshi terefden satish yaradilmasi uchun melumat alir
            Console.WriteLine("==============Existing product codes are shown below:==============");
            foreach (var item in market.Products)
            {
                Console.WriteLine($"code - {item.Code} ({item.Name})");
            }
            Console.WriteLine("Enter the code of the product, please");
            int code;
            string codestr = Console.ReadLine();
            while (!int.TryParse(codestr, out code))
            {
                Console.WriteLine("Insert the code again");
                codestr = Console.ReadLine();
            }
            Console.WriteLine("Enter the quantity of the product, please");
            int quantity;
            string quantitystr = Console.ReadLine();
            while (!int.TryParse(quantitystr, out quantity))
            {
                Console.WriteLine("Insert the quantity again");
                quantitystr = Console.ReadLine();
            }
            try
            {
                market.AddInvoice(code, quantity); 
            }
            catch (Exception e)
            {
               Console.WriteLine("Something went wrong. Try again, please");
               Console.WriteLine(e.Message);
            }   
        }
        public static void AddReturnProductMenu()
        {
            //Bu metod satihsdan mehsulu qaytarmag uchun qarshi terefden melumat alir
            Console.WriteLine("==============Existing invoice numbers are shown below:==============");
            foreach (var item in market.Invoices)
            {
                Console.WriteLine($"number - {item.Number}");
            }
            Console.WriteLine("Enter the number of the invoice, please");
            int number;
            string numberstr = Console.ReadLine();
            while (!int.TryParse(numberstr, out number))
            {
                Console.WriteLine("Insert the number again");
                numberstr = Console.ReadLine();
            }
            Console.WriteLine("Enter the name of the product, please");
            string name = Console.ReadLine().ToLower();
            Console.WriteLine("Enter the quantity of the product, please");
            int quantity;
            string quantitystr = Console.ReadLine();
            while (!int.TryParse(quantitystr, out quantity))
            {
                Console.WriteLine("Insert the quantity again");
                quantitystr = Console.ReadLine();
            }
            try
            {
                market.ReturnProduct(number, name, quantity);
                Console.WriteLine("==============Product/products returned==============");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again, please");
                Console.WriteLine(e.Message);
            }  
        }
        public static void AddDeleteInvoiceMenu()
        {
            //Bu metod qarshi terefden satishi silmek uchun satish haqqinda melumat alir(yani satishin nomresi)
            Console.WriteLine("Existing invoice numbers are shown below:");
            foreach (var item in market.Invoices)
            {
                Console.WriteLine($"number - {item.Number}");
            }
            Console.WriteLine("Enter the number of the invoice, please");
            int number;
            string numberstr = Console.ReadLine();
            while (!int.TryParse(numberstr, out number))
            {
                Console.WriteLine("Insert the number again");
                numberstr = Console.ReadLine();
            }
            try
            {
                market.DeleteInvoice(number);
                Console.WriteLine("==============Invoice deleted==============");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again, please");
                Console.WriteLine(e.Message);
            }  
        }
        #endregion 
    }
}
