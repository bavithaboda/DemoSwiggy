using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSwiggy.Models;

namespace DemoSwiggy.Interfaces
{
    public interface ISupportEmp
    {
        PaginatedList<Customer> GetItems(string SortProperty, string sortOrder, string SearchText, int pageIndex = 1, int pageSize = 5);
        Customer GetCustomer(int id);
        Customer Create(Customer customers);
        Customer Edit(Customer customers);
        Customer Delete(Customer customers);
        Customer Details(Customer customers);
        public bool IsCustomerNameExists(string name);
        public bool IsCustomerNameExists(string name, int id);
    }
}
