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
        public void AddProduct(string name, double price, int code, string category, int quantity)
        {
                 
            Product product = new();
            product.Name = name;
            product.Price = price;
            product.Code = code;
            product.Quantity = quantity;
            product.Category = category;
            Products.Add(product);

        }
        public void ChangeProductByCode(int code, string name, double price, int codenew, int quantity, string category)
        {
            var index = Products.FindIndex(s => s.Code == code);
            Products.RemoveAt(index);
            Product product = new();
            product.Name = name;
            product.Price = price;
            product.Code = codenew;
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
            
            Product product = Products.FirstOrDefault(s => s.Code == code);
            SoldProduct sold = new();
            Invoice invoice = new();
            product.Quantity = product.Quantity - quantity;
            invoice.Cost = product.Price * quantity;
            invoice.SoldProducts.Add(sold);
            Invoices.Add(invoice);

           
        }
        public void ReturnProduct(string name, int quantity)
        {
            Product product = new();
            SoldProduct sold = new();
            Invoice invoice = new();
            invoice.SoldProducts.Find(s=> s.Product.Name == name && s.Product.Quantity >= quantity);
            product.Quantity = product.Quantity + quantity;
            invoice.Cost = invoice.Cost - (product.Price * quantity);
            invoice.SoldProducts.Remove(sold);
            Invoices.Remove(invoice);

        }
        public List<Invoice> ReturnInvoices()
        {
            return Invoices.ToList();
        }
        public void DeleteInvoice(int no)
        {
            Invoice invoice = new();
            int index = Invoices.FindIndex(s => s.Number == no);
            Invoices.RemoveAt(index);
        }
        public List<Invoice> SearchByDate(DateTime startdate, DateTime enddate)
        {
            var result = Invoices.Where(m => m.Date >= startdate && m.Date <= enddate);
            return (List<Invoice>)result;
        }
        public List<Invoice> SearchInvoiceByPrice(double startcost, double endcost)
        {
            var index = Invoices.FindAll(s => s.Cost >= startcost && s.Cost <= endcost);
            return index;
        }
        public List<Invoice> SearchByNumber(int no)
        {
            var res = Invoices.FindAll(s => s.Number == no);
            return res;            
        }
        public List<Invoice> SearchByOnlyDate(DateTime date)
        {
            var res = Invoices.Where(s => s.Date == date);
            return (List<Invoice>)res;
        }
        #endregion
        



    }
}
