using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Services;
namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            ICustomerRepositoy customer = new CustomerRepository();
            var myList = customer.GetAllCustomers();
            Console.WriteLine(" ${myList} aaaaaaaa"  );
            Console.ReadKey();
         /*   Customers AddCustomer = new Customers() 

            {
                FullName = "homazargar",
                CustomerImage = "noPhoto",
                Mobile = "0000000000"
            };

            customer.InsertCustomer(AddCustomer);
            customer.Save(); */
        }
    }
}
