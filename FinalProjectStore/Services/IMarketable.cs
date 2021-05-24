using FinalProjectStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProjectStore.Services
{
    public interface IMarketable
    {
       

        #region Product
        public void AddProduct(string name, double price, int code, string category, int quantity)
        {
          

        }
        public void ChangeProductByCode(int code, string name, double price, int codenew, int quantity, string category)
        {
            

        }
        public void DeleteProductByCode(int code)
        {
           

        }
        public void SearchProductByPrice(double startprice, double endprice)
        {
            
        }
        public void SearchByCategory(string category)
        {
            

        }
        public void SearchByName(string name)
        {
          
        }
        #endregion

        #region Invoice
        public void AddInvoice(string name, int quantity, double cost)
        {
            

        }
        public void ReturnProduct(string name, int quantity)
        {
           

        }
        public void ReturnInvoices()
        {
           
        }
        public void DeleteInvoice(int no)
        {
           
        }
        public void SearchByDate(DateTime startdate, DateTime enddate)
        {
            
        }
        public void SearchInvoiceByPrice(double startcost, double endcost)
        {
            
        }
        public void SearchByNumber(int no)
        {
           
        }
        public void SearchByOnlyDate(DateTime date)
        {

        }
        #endregion
    }
}
