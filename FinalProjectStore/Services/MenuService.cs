using ConsoleTables;
using FinalProjectStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var table = new ConsoleTable("Names","Code", "Category", "Price", "Quantity");
            var input = Console.ReadLine();
            foreach (var item in market.SearchByCategory(input))
            {
                table.AddRow(item);
            }
            
            table.Write();
            Console.WriteLine();

                     
        }
        public static void DisplayProductByPrice()
        {
            var table = new ConsoleTable("Names", "Code", "Category", "Price", "Quantity");
            var startprice = double.Parse(Console.ReadLine());
            var endprice = double.Parse(Console.ReadLine());
            foreach (var item in market.SearcProductByPrice(startprice, endprice))
            {
                table.AddRow(item);
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
                table.AddRow(item);
            }
            table.Write();
            Console.WriteLine();
        }

        public static void DisplayInvoices()
        {
            var table = new ConsoleTable("Number", "Cost", "Quantity", "Date");
            foreach (var item in market.ReturnInvoices())
            {
                table.AddRow(item);

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
                table.AddRow(item);
            }
            table.Write();
            Console.WriteLine();
        }

        public void DisplayInvoiceByCost()
        {
            var table = new ConsoleTable("Number", "Cost", "Quantity", "Date");
            double startcost = double.Parse(Console.ReadLine());
            double endcost = double.Parse(Console.ReadLine());
            foreach (var item in market.SearchInvoiceByPrice(startcost, endcost))
            {
                table.AddRow(item);
            }
            table.Write();
            Console.WriteLine();
        }

        public void DisplayInvoiceByNo()
        {
            var table = new ConsoleTable("Number", "Cost", "Quantity", "Date");
            int no = int.Parse(Console.ReadLine());
            foreach (var item in market.SearchByNumber(no))
            {
                table.AddRow(item);
            }
            table.Write();
            Console.WriteLine();
        }
        public void DisplayInvoiceByOnlyDate()
        {
            var table = new ConsoleTable("Number", "Cost", "Quantity", "Date");
            DateTime date = DateTime.Parse(Console.ReadLine());
            foreach (var item in market.SearchByOnlyDate(date))
            {
                table.AddRow(item);
            }
            table.Write();
            Console.WriteLine();
        }
        #endregion
        public void AddProductMenu()
        {
            string name = Console.ReadLine();
            double price = double.Parse(Console.ReadLine());
            int code = int.Parse(Console.ReadLine());
            string category = Console.ReadLine();
            int quantity = int.Parse(Console.ReadLine());
            market.AddProduct(name, price, code, category, quantity);
        }
        public void AddChangeProductMenu()
        {
            int code = int.Parse(Console.ReadLine());
            string name = Console.ReadLine();
            double price = double.Parse(Console.ReadLine());
            int codenew = int.Parse(Console.ReadLine());
            int quantity = int.Parse(Console.ReadLine());
            string category = Console.ReadLine();
            market.ChangeProductByCode(code, name, price, codenew, quantity, category);
        }
        public void AddRemoveProductMenu()
        {
            int code = int.Parse(Console.ReadLine());
            market.DeleteProductByCode(code);
        }
        public void AddInvoiceMenu()
        {
            string name = Console.ReadLine();
            int quantity = int.Parse(Console.ReadLine());
            double cost = double.Parse(Console.ReadLine());
            market.AddInvoice(name, quantity, cost);
        }
        public void AddReturnProductMenu()
        {
            string name = Console.ReadLine();
            int quantity = int.Parse(Console.ReadLine());
            market.ReturnProduct(name, quantity);
        }
        public void AddDeleteInvoiceMenu()
        {
            int no = int.Parse(Console.ReadLine());
            market.DeleteInvoice(no);
        }
    }
}
