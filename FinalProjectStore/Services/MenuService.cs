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
                table.AddRow(product.Code, product.Name, product.Category, product.Price, product.Quantity);
            }

            table.Write();
            Console.WriteLine();
        }

        public static void DisplayProductsByCategory()
        {
            var table = new ConsoleTable("Name", "Code", "Category", "Price", "Quantity");
            var input = Console.ReadLine();
            foreach (var item in market.SearchByCategory(input))
            {
                table.AddRow(item.Name, item.Code, item.Category, item.Price, item.Quantity);
            }

            table.Write();
            Console.WriteLine();


        }
        public static void DisplayProductByPrice()
        {
            var table = new ConsoleTable("Name", "Code", "Category", "Price", "Quantity");
            var startprice = double.Parse(Console.ReadLine());
            var endprice = double.Parse(Console.ReadLine());
            foreach (var item in market.SearcProductByPrice(startprice, endprice))
            {
                table.AddRow(item.Name, item.Code, item.Category, item.Price, item.Quantity);
            }
            table.Write();
            Console.WriteLine();
        }
        public static void DisplayProductByName()
        {
            var table = new ConsoleTable("Names", "Code", "Category", "Price", "Quantity");
            var name = Console.ReadLine();
            foreach (var item in market.SearchByName(name))
            {
                table.AddRow(item.Name, item.Code, item.Category, item.Price, item.Quantity);
            }
            table.Write();
            Console.WriteLine();

        }

        public static void DisplayInvoices()
        {
            var table = new ConsoleTable("Number", "Cost", "Quantity");
            foreach (var item in market.ReturnInvoices())
            {
                table.AddRow(item.Number, item.Cost, item.SoldProducts.Count);

            }
            table.Write();

            Console.WriteLine();
        }
        public static void DisplayInvoicesByDate()
        {
            var table = new ConsoleTable("Number", "Cost", "Quantity", "Date");
            DateTime startdate = DateTime.Parse(Console.ReadLine());
            DateTime enddate = DateTime.Parse(Console.ReadLine());
            foreach (var item in market.SearchByDate(startdate, enddate))
            {
                table.AddRow(item.Number, item.Cost, item.SoldProducts.Count, item.Date);
            }
            table.Write();
            Console.WriteLine();
        }

        public static void DisplayInvoiceByCost()
        {
            var table = new ConsoleTable("Number", "Cost", "Date");
            double startcost = double.Parse(Console.ReadLine());
            double endcost = double.Parse(Console.ReadLine());
            foreach (var item in market.SearchInvoiceByPrice(startcost, endcost))
            {
                table.AddRow(item.Number, item.Cost, item.Date);
            }
            var table1 = new ConsoleTable("Quantity");
            foreach (var item in market.Invoices)
            {
                table1.AddRow(item.SoldProducts.Count);
            }
            table.Write();
            table1.Write();
            Console.WriteLine();
        }

        public static void DisplayInvoiceByNo()
        {
            var table = new ConsoleTable("Number", "Cost", "Date");
            int no = int.Parse(Console.ReadLine());
            foreach (var item in market.SearchByNumber(no))
            {
                table.AddRow(item.Number, item.Cost, item.Date);

            }
            var table1 = new ConsoleTable("Name", "Price", "Quantity");
            foreach (var invoice in market.Invoices)
            {
                foreach (var soldProduct in invoice.SoldProducts.ToList())
                {
                    table1.AddRow(soldProduct.Product.Name, soldProduct.Product.Price, soldProduct.quantity);
                }
            }
            table.Write();
            table1.Write();
            Console.WriteLine();
        }
        public static void DisplayInvoiceByOnlyDate()
        {
            var table = new ConsoleTable("Number", "Cost", "Quantity", "Date");
            DateTime date = DateTime.Parse(Console.ReadLine());

            foreach (var item in market.SearchByOnlyDate(date))
            {
                table.AddRow(item.Number, item.Cost, item.SoldProducts.Count, item.Date);
            }
            table.Write();
            Console.WriteLine();
        }
        #endregion
        public static void AddProductMenu()
        {
            Console.WriteLine("Enter the name of the product, please");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the price of the product, please");
            double price = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter the category of the product, please");
            string category = Console.ReadLine();
            if (category != Category.Baby.ToString() && category != Category.Bakery.ToString() && category != Category.Beverages.ToString() && category != Category.Canned.ToString() && category != Category.Baby.ToString())
                throw new KeyNotFoundException();
            Console.WriteLine("Enter the quantity of the product, please");
            int quantity = int.Parse(Console.ReadLine());
            market.AddProduct(name, price, category, quantity);
            Console.WriteLine("Product added");
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
            if (category != Category.Baby.ToString() && category != Category.Bakery.ToString() && category != Category.Beverages.ToString() && category != Category.Canned.ToString() && category != Category.Baby.ToString())
                throw new KeyNotFoundException();
            market.ChangeProductByCode(code, name, price, quantity, category);
            Console.WriteLine("Product changed");
        }
        public static void AddRemoveProductMenu()
        {
            Console.WriteLine("Enter the code of the product, please");
            int code = int.Parse(Console.ReadLine());
            market.DeleteProductByCode(code);
        }
        public static void AddInvoiceMenu()
        {
            Console.WriteLine("Enter the code of the product, please");
            int code = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the quantity of the product, please");
            int quantity = int.Parse(Console.ReadLine());
            
            market.AddInvoice(code, quantity);
        }
        public static void AddReturnProductMenu()
        {
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the code of the product, please");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the quantity of the product, please");
            int quantity = int.Parse(Console.ReadLine());
            market.ReturnProduct(number, name, quantity);
            Console.WriteLine("Product/products returned");
        }
        public static void AddDeleteInvoiceMenu()
        {
            Console.WriteLine("Enter the number of the invoice, please");
            int no = int.Parse(Console.ReadLine());
            market.DeleteInvoice(no);
        }
    }
}
