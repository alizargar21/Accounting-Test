﻿using Accounting.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Repositories
{
    public interface ICustomerRepositoy
    {
        List<Customers> GetAllCustomers();

        IEnumerable<Customers> GetCustomersByFilter(string parameter);
       
        List<ListCustomerViewModel> GetNamesCustomers(string filter = "");
       
        Customers GetCustomerById(int customerId);

        bool InsertCustomer(Customers customer);

        bool UpdateCustomer(Customers customer);

        bool DeleteCustomer(Customers customer);
        // Overload for Delete
        bool DeleteCustomer(int customerId);

        int GetCustomerIdByName (string name);

    }
}
