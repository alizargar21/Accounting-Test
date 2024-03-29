
namespace Accounting.DataLayer
{
    using System;
    using System.Collections.Generic;

    public partial class Customers
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CustomerImage { get; set; }
    }
}
