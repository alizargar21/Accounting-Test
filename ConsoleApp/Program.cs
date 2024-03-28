using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Services;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer;
using Accounting.DataLayer.Context;
namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
        UnitOfWork dbContext = new UnitOfWork();


            Customers newCustomer = new Customers()
            {
                FullName = "Hamid",
                Mobile = "16548461312",
                Email = "2663666",
                Address = "asdasdasdisfbnjadf"
            };

            dbContext.CustomerRepositoy.InsertCustomer(newCustomer);

            var list = dbContext.CustomerRepositoy.GetAllCustomers();
          
           

        }
    }
}
