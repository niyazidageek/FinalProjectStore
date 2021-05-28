using ConsoleTables;
using FinalProjectStore.Data.Entities;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FinalProjectStore.Services
{
    public class MenuService
    {
        static MarketMenu market = new();



        #region Display
        public static void DisplayProducts()
        {
            var table = new ConsoleTable("Code", "Name", "Category", "Price", "Quantity");

            foreach (var product in market.Products)
            {
                table.AddRow(product.Code, product.Name, product.Category, product.Price.ToString("#.00"), product.Quantity);
            }

            table.Write();
            Console.WriteLine();
        }

        public static void DisplayProductsByCategory()
        {
            var table = new ConsoleTable("Name", "Code", "Category", "Price", "Quantity");
            Console.WriteLine("Insert the category, please");
            var category = Console.ReadLine();
            foreach (var item in market.SearchByCategory(category))
            {
                table.AddRow(item.Name, item.Code, item.Category, item.Price.ToString("#.00"), item.Quantity);
            }

            table.Write();
            Console.WriteLine();


        }
        public static void DisplayProductByPrice()
        {
            var table = new ConsoleTable("Name", "Code", "Category", "Price", "Quantity");
            Console.WriteLine("Insert the minimum price of the product, please");
            var startprice = double.Parse(Console.ReadLine());
            Console.WriteLine("Insert the maximum price of the product, please");
            var endprice = double.Parse(Console.ReadLine());
            foreach (var item in market.SearcProductByPrice(startprice, endprice))
            {
                table.AddRow(item.Name, item.Code, item.Category, item.Price.ToString("#.00"), item.Quantity);
            }
            table.Write();
            Console.WriteLine();
        }
        public static void DisplayProductByName()
        {
            var table = new ConsoleTable("Names", "Code", "Category", "Price", "Quantity");
            Console.WriteLine("Insert the name of the product, please");
            var name = Console.ReadLine();
            foreach (var item in market.SearchByName(name))
            {
                table.AddRow(item.Name, item.Code, item.Category, item.Price.ToString("#.00"), item.Quantity);
            }
            table.Write();
            Console.WriteLine();

        }

        public static void DisplayInvoices()
        {
            var table = new ConsoleTable("Number", "Cost", "Quantity of the product types", "Status");
            foreach (var item in market.ReturnInvoices())
            {
                table.AddRow(item.Number, item.Cost.ToString("#.00"), item.SoldProducts.Count, item.Status);

            }
            table.Write();

            Console.WriteLine();
        }
        public static void DisplayInvoicesByDate()
        {
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
            foreach (var item in market.SearchByDate(startdate, enddate))
            {
                table.AddRow(item.Number, item.Cost.ToString("#.00"), item.SoldProducts.Count, item.Date, item.Status);
            }
            table.Write();
            Console.WriteLine();
        }

        public static void DisplayInvoiceByCost()
        {
            var table = new ConsoleTable("Number", "Cost", "Quantity of the product types", "Date", "Status");
            Console.WriteLine("Insert the minimum cost, please");
            double startcost = double.Parse(Console.ReadLine());
            Console.WriteLine("Insert the maximum cost, please");
            double endcost = double.Parse(Console.ReadLine());
            foreach (var item in market.SearchInvoiceByPrice(startcost, endcost))
            {
                table.AddRow(item.Number, item.Cost.ToString("#.00"), item.SoldProducts.Count, item.Date, item.Status);
            }
            
            table.Write();
            Console.WriteLine();
        }

        public static void DisplayInvoiceByNo()
        {
            var table = new ConsoleTable("Number", "Cost", "Date", "Status");
            Console.WriteLine("Insert the number of the invoice, please");
            int no = int.Parse(Console.ReadLine());
            foreach (var item in market.SearchByNumber(no))
            {
                table.AddRow(item.Number, item.Cost.ToString("#.00"), item.Date, item.Status);

            }
            var table1 = new ConsoleTable("Name", "Price", "Quantity");
            var index = market.Invoices.FindIndex(s=>s.Number == no);
            var res = market.Invoices.ElementAt(index);
            foreach (var invoice in res.SoldProducts)
            {              
                    table1.AddRow(invoice.Product.Name, invoice.Product.Price.ToString("#.00"), invoice.quantity);      
            }
            table.Write();
            table1.Write();
            Console.WriteLine();
        }
        public static void DisplayInvoiceByOnlyDate()
        {
            DateTime date;
            var table = new ConsoleTable("Number", "Cost", "Quantity of the product types", "Date", "Status");
            Console.WriteLine("Insert the date of the invoice, please (mm.dd.yyyy)");
            string datestr = Console.ReadLine();
            while(!DateTime.TryParse(datestr, out date))
            {
                Console.WriteLine("Date format is not correct. Insert the date again (mm.dd.yyyy)");
                datestr = Console.ReadLine();
            }
            foreach (var item in market.SearchByOnlyDate(date))
            {
                table.AddRow(item.Number, item.Cost.ToString("#.00"), item.SoldProducts.Count, item.Date, item.Status);
            }
            table.Write();
            Console.WriteLine();
        }
        #endregion

        #region Addition/Removals
        public static void AddProductMenu()
        {
            Console.WriteLine("Enter the name of the product, please");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the price of the product, please");
            double price = double.Parse(Console.ReadLine());
            
            Console.WriteLine("=============Existing categories are below:==============");
            var categories = Enum.GetValues(typeof(Category));
            foreach (var item in categories)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Enter the name of the product's category, please");
            string category = Console.ReadLine();
            if (category.ToLower() != Category.Baby.ToString().ToLower() && category.ToLower() != Category.Bakery.ToString().ToLower() && category.ToLower() != Category.Beverages.ToString().ToLower() && category.ToLower() != Category.Canned.ToString().ToLower() && category.ToLower() != Category.Baby.ToString().ToLower())
                throw new KeyNotFoundException();
            Console.WriteLine("Enter the quantity of the product, please");
            int quantity = int.Parse(Console.ReadLine());
            try
            {
                market.AddProduct(name, price, category, quantity);
                Console.WriteLine("Product added");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again, please");
                Console.WriteLine(e.Message);
            }
            
        }
        public static void AddChangeProductMenu()
        {
            Console.WriteLine("Enter the code of the product, please");
            int code = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the new name of the product, please");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the new price of the product, please");
            double price = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter the new quantity of the product, please");
            int quantity = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the new category of the product, please");
            string category = Console.ReadLine();
            if (category.ToLower() != Category.Baby.ToString().ToLower() && category.ToLower() != Category.Bakery.ToString().ToLower() && category.ToLower() != Category.Beverages.ToString().ToLower() && category.ToLower() != Category.Canned.ToString().ToLower() && category.ToLower() != Category.Baby.ToString().ToLower())
                throw new KeyNotFoundException();
            var values = Enum.GetValues(typeof(Category));
            foreach (var item in values)
            {
                if (item.ToString().ToLower() != category.ToLower())
                    throw new KeyNotFoundException();
            }
            try
            {
                market.ChangeProductByCode(code, name, price, quantity, category);
                Console.WriteLine("Product changed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again, please");
                Console.WriteLine(e.Message);
            }
            
        }
        public static void AddRemoveProductMenu()
        {
            Console.WriteLine("Enter the code of the product, please");
            int code = int.Parse(Console.ReadLine());
            try
            {
                market.DeleteProductByCode(code);
                Console.WriteLine("Product deleted");
            }
            catch (Exception e)
            {
               Console.WriteLine("Something went wrong. Try again, please");
               Console.WriteLine(e.Message);
            }
            
        }
        public static void AddInvoiceMenu()
        {
            Console.WriteLine("Enter the code of the product, please");
            int code = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the quantity of the product, please");
            int quantity = int.Parse(Console.ReadLine());
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
            Console.WriteLine("Enter the number of the invoice, please");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the name of the product, please");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the quantity of the product, please");
            int quantity = int.Parse(Console.ReadLine());
            try
            {
                market.ReturnProduct(number, name, quantity);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Try again, please");
                Console.WriteLine(e.Message);
            }
            
        }
        public static void AddDeleteInvoiceMenu()
        {
            Console.WriteLine("Enter the number of the invoice, please");
            int no = int.Parse(Console.ReadLine());
            try
            {
                market.DeleteInvoice(no);
                Console.WriteLine("Invoice deleted");
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
