using FinalProjectStore.Data.Entities;
using System;
using System.Collections.Generic;

namespace FinalProjectStore.Services
{
    //Ashaghda interface teqdim olunub. Interface-in ichinde ishledeceyimiz metodlarin adlari ve overloading-lari verilib.
    public interface IMarketable
    {
        #region Product
        public void AddProduct(string name, double price, string category, int quantity);
        public void ChangeProductByCode(int code, string name, double price, int quantity, string category);
        public void DeleteProductByCode(int code);
        public List<Product> SearchProductByPrice(double startprice, double endprice);
        public List<Product> SearchByCategory(string category);
        public List<Product> SearchByName(string name);
        public List<Product> ReturnProducts();
        #endregion

        #region Invoice
        public void AddInvoice(int code, int quantity);
        public void ReturnProduct(int number, string name, int quantity);
        public List<Invoice> ReturnInvoices();
        public void DeleteInvoice(int no);
        public List<Invoice> SearchByDate(DateTime startdate, DateTime enddate);
        public List<Invoice> SearchInvoiceByPrice(double startcost, double endcost);
        public List<Invoice> SearchByNumber(int no);
        public List<Invoice> SearchByOnlyDate(DateTime date);
        #endregion
    }
}
