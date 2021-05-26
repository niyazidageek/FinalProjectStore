using FinalProjectStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                 
            Product product = new();
            product.Name = name;
            product.Price = price;
            
            product.Quantity = quantity;
            product.Category = category;
            Products.Add(product);

        }
        public void ChangeProductByCode(int code, string name, double price, int quantity, string category)
        {
            var index = Products.FindIndex(s => s.Code == code);
            Products.RemoveAt(index);
            Product product = new();
            product.Name = name;
            product.Price = price;
            
            product.Quantity = quantity;
            product.Category = category;
            Products.Add(product);

        }
        public void DeleteProductByCode(int code)
        {
            var index = Products.FindIndex(s => s.Code == code);
            Products.RemoveAt(index);

        }
        public List<Product> SearcProductByPrice(double startprice, double endprice)
        {
            var res = Products.FindAll(s => s.Price >= startprice && s.Price <= endprice);
            return res.ToList();
        }
        public List<Product> SearchByCategory(string category)
        {
            //if (Category.Baby.ToString() == category)
            //{

            //}
            var res = Products.FindAll(s => s.Category == category);
            return res.ToList();

        }
        public List<Product> SearchByName(string name)
        {
            var temp = Products.FindAll(s => s.Name == name);
            
            return temp.ToList();
        }
        #endregion

        #region Invoice
        public void AddInvoice(int code, int quantity)
        {
            int option = 0;
            Invoice invoice = new();
            Product product = Products.FirstOrDefault(s => s.Code == code);
            SoldProduct sold = new(product);           
            product.Quantity = product.Quantity - quantity;
            invoice.Cost += product.Price * quantity;
            sold.quantity += quantity;
            invoice.SoldProducts.Add(sold);
            


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
                        code = int.Parse(Console.ReadLine());
                        quantity = int.Parse(Console.ReadLine());
                        product = Products.FirstOrDefault(s => s.Code == code);
                        sold = new(product);
                        product.Quantity = product.Quantity - quantity;
                        invoice.Cost += product.Price * quantity;
                        invoice.SoldProducts.Add(sold);                        
                        break;
                    case 2:
                        Console.WriteLine("Invoice added");
                        Invoices.Add(invoice);
                        break;
                }
            } while (option!=2);
            

           
        }
        public void ReturnProduct(int number, string name, int quantity)
        {
            Product product = Products.FirstOrDefault(s => s.Name == name);
            Invoice invoice = Invoices.FirstOrDefault(s=> s.Number == number);
            SoldProduct sold = invoice.SoldProducts.FirstOrDefault(s => s.quantity >= quantity);

            Invoices.Remove(invoice);
            invoice.SoldProducts.Remove(sold);
            product.Quantity = product.Quantity + quantity;
            invoice.Cost -= product.Price * quantity;
            sold.quantity -= quantity;
            Invoices.Add(invoice);
            invoice.SoldProducts.Add(sold);
            

            
            

        }
        public List<Invoice> ReturnInvoices()
        {
            return Invoices.ToList();
        }
        
        public void DeleteInvoice(int no)
        {
            Invoice invoice = Invoices.FirstOrDefault(s=> s.Number == no);
            
            Product product = new();
            SoldProduct sold = new(product);
            foreach (var item in invoice.SoldProducts)
            {
                product.Quantity += item.Quantity;
            }
            Invoices.Remove(invoice);
        }
        public List<Invoice> SearchByDate(DateTime startdate, DateTime enddate)
        {
            var result = Invoices.Where(m => m.Date>= startdate && m.Date <= enddate);
            return result.ToList();
        }
        public List<Invoice> SearchInvoiceByPrice(double startcost, double endcost)
        {
            var index = Invoices.FindAll(s => s.Cost >= startcost && s.Cost <= endcost);
            return index.ToList();
        }
        public List<Invoice> SearchByNumber(int no)
        {
            var res = Invoices.FindAll(s => s.Number == no);
            return res;            
        }
        public List<Invoice> SearchByOnlyDate(DateTime date)
        {
            var res = Invoices.Where(s => s.Date.Day == date.Day && s.Date.Month == date.Month && s.Date.Month == date.Month);
            return res.ToList();
        }
        #endregion
        



    }
}
