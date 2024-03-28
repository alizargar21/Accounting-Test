using Accounting.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Accounting.DataLayer.Services
{
    public class CustomerRepository : ICustomerRepositoy
    {
        // Dependency Injection
        private Accounting_DBEntities db;

        // Create Constructor

        public CustomerRepository(Accounting_DBEntities context)
        {
            db = context;
        }

        public bool DeleteCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Deleted;
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                var customer = GetCustomerById(customerId);
                DeleteCustomer(customer);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public List<Customers> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        public Customers GetCustomerById(int customerId)
        {
            return db.Customers.Find(customerId);
        }

        public bool InsertCustomer(Customers customer)
        {


          
             try
             {
              
                db.Customers.Add(customer);
                db.SaveChanges();
                 return true;

             }
             catch
             {
                 return false;
             } 
        }

     

        public bool UpdateCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State= EntityState.Modified;
                return true;

            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Customers> GetCustomersByFilter(string parameter)
        {
            return db.Customers.Where(c => c.FullName.Contains(parameter) || c.Mobile.Contains(parameter) || c.Email.Contains(parameter));
        }
    }
}
