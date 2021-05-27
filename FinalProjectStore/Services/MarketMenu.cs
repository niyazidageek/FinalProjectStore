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
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Product name");
            if (price <= 0)
                throw new ArgumentOutOfRangeException("Product price");
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException("Product category");
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("Product quantity");
                 
            Product product = new();
            product.Name = name; //.First().ToString().ToUpper() + name.Substring(1);
            product.Price = price;           
            product.Quantity = quantity;
            product.Category = category.ToLower();
            Products.Add(product);

        }
        public void ChangeProductByCode(int code, string name, double price, int quantity, string category)
        {
            var index = Products.FindIndex(s => s.Code == code);
            if (index == -1)
                throw new KeyNotFoundException();
            if (code <= 0)
                throw new ArgumentOutOfRangeException("Product code");
            if (price <= 0)
                throw new ArgumentOutOfRangeException("Product price");
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("Product quantity");
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException("Product category");
            Products.RemoveAt(index);
            Product product = new();
            product.Name = name;
            product.Price = price;       
            product.Quantity = quantity;
            product.Category = category.ToLower();
            Products.Add(product);

        }
        public void DeleteProductByCode(int code)
        {
            var index = Products.FindIndex(s => s.Code == code);
            if (index == -1)
                throw new KeyNotFoundException();
            Products.RemoveAt(index);

        }
        public List<Product> SearcProductByPrice(double startprice, double endprice)
        {
            if (startprice <= 0)
                throw new ArgumentOutOfRangeException("Product start price");
            if (endprice <= 0)
                throw new ArgumentOutOfRangeException("Product end price");
            var res = Products.FindAll(s => s.Price >= startprice && s.Price <= endprice);
            if (res == null)
                throw new KeyNotFoundException();
            return res.ToList();
        }
        public List<Product> SearchByCategory(string category)
        {

            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException("Product category");
            var res = Products.FindAll(s => s.Category == category.ToLower());
            if (res == null)
                throw new KeyNotFoundException();
            return res.ToList();

        }
        public List<Product> SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Product  name");
            var temp = Products.FindAll(s => s.Name == name);
            if (temp == null)
                throw new KeyNotFoundException();
            
            return temp.ToList();
        }
        #endregion

        #region Invoice
        public void AddInvoice(int code, int quantity)
        {
            if (code <= 0)
                throw new ArgumentOutOfRangeException("Product code");
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("Product quantity");
            int option = 0;
            Invoice invoice = new();
            Product product = Products.FirstOrDefault(s => s.Code == code);
            if (product == null)
                throw new KeyNotFoundException();
            SoldProduct sold = new(product);           
            product.Quantity = product.Quantity - quantity;
            invoice.Cost += product.Price * quantity;
            sold.quantity += quantity;
            invoice.SoldProducts.Add(sold);
            invoice.Status = "Exists";
            


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
                        if (code <= 0)
                            throw new ArgumentOutOfRangeException("Product code");
                        quantity = int.Parse(Console.ReadLine());
                        if (quantity <= 0)
                            throw new ArgumentOutOfRangeException("Product quantity");
                        product = Products.FirstOrDefault(s => s.Code == code);
                        if (product == null)
                            throw new KeyNotFoundException();
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
            if (number <= 0)
                throw new ArgumentOutOfRangeException("Invoice number");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Product name");
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("Product quantity");
            Product product = Products.FirstOrDefault(s => s.Name == name);
            if (product == null)
                throw new KeyNotFoundException();
            Invoice invoice = Invoices.FirstOrDefault(s=> s.Number == number);
            if (invoice == null)
                throw new KeyNotFoundException();
            SoldProduct sold = invoice.SoldProducts.FirstOrDefault(s => s.quantity >= quantity);
            if (sold == null)
                throw new KeyNotFoundException();

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
            if (no <= 0)
                throw new ArgumentOutOfRangeException("Invoice number");
            
            Invoice invoice = Invoices.FirstOrDefault(s=> s.Number == no);
            if (invoice == null)
                throw new KeyNotFoundException();

            var res = invoice.SoldProducts.ToList();
            //SoldProduct sold = new(product);
            foreach (var item in res)
            {
                item.Product.Quantity += item.quantity;
            }
            invoice.Status = "Deleted";
            
        }
        public List<Invoice> SearchByDate(DateTime startdate, DateTime enddate)
        {
            
            var result = Invoices.Where(m => m.Date>= startdate && m.Date <= enddate);
            if (result == null)
                throw new KeyNotFoundException();
            return result.ToList();
        }
        public List<Invoice> SearchInvoiceByPrice(double startcost, double endcost)
        {
            if (startcost <= 0)
                throw new ArgumentOutOfRangeException("Invocie start cost");
            if (endcost <= 0)
                throw new ArgumentOutOfRangeException("Invocie end cost");
            var index = Invoices.FindAll(s => s.Cost >= startcost && s.Cost <= endcost);
            if (index == null)
                throw new KeyNotFoundException();
            return index.ToList();
        }
        public List<Invoice> SearchByNumber(int no)
        {
            if (no <= 0)
                throw new ArgumentOutOfRangeException("Invoice number");
            var res = Invoices.FindAll(s => s.Number == no);
            if (res == null)
                throw new KeyNotFoundException();
            return res;            
        }
        public List<Invoice> SearchByOnlyDate(DateTime date)
        {
            var res = Invoices.Where(s => s.Date.Day == date.Day && s.Date.Month == date.Month && s.Date.Month == date.Month && s.Date.Year == date.Year);
            if (res == null)
                throw new KeyNotFoundException();
            return res.ToList();
        }
        #endregion
        



    }
}
