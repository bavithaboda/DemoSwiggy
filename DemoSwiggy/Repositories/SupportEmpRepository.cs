using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSwiggy.Data;
using DemoSwiggy.Models;
using DemoSwiggy.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace DemoSwiggy.Repositories
{
    public class SupportEmpRepository :ISupportEmp
    {
        private readonly SwiggyDbContext _context;
        List<Customer> sortlist = null;
        public SupportEmpRepository(SwiggyDbContext context)
        {
            _context = context;
        }

        public Customer Create(Customer customer)
        {
            _context.customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer Delete(Customer customers)
        {
            _context.customers.Attach(customers);
            _context.Entry(customers).State = EntityState.Deleted;
            _context.SaveChanges();
            return customers;
        }

        public Customer Details(Customer customers)
        {
            throw new NotImplementedException();
        }

        public Customer Edit(Customer customers)
        {
            _context.customers.Attach(customers);
            _context.Entry(customers).State = EntityState.Modified;
            _context.SaveChanges();
            return customers;
        }

        public Customer GetCustomer(int id)
        {
            Customer customer = _context.customers.Where(u => u.CustomerId == id).FirstOrDefault();
            return customer;
        }
        public PaginatedList<Customer> GetItems(string SortProperty, string sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5)
        {
            List<Customer> customers;
            if (SearchText != "" && SearchText != null)
            {
                customers = _context.customers.Where(n => n.Name.Contains(SearchText) || n.City.Contains(SearchText)).ToList();
            }
            else
                customers = _context.customers.ToList();
            customers = DoSort(customers, SortProperty, sortOrder);
            PaginatedList<Customer> retCustomers = new PaginatedList<Customer>(customers, pageIndex, pageSize);

            return retCustomers;
        }

        private List<Customer> DoSort(List<Customer> customers, string SortProperty, string sortOrder)
        {
            if (SortProperty.ToLower() == "name")
            {
                if (sortOrder == "Ascending")
                    sortlist = customers.OrderBy(n => n.Name).ToList();
                else
                    sortlist = customers.OrderByDescending(n => n.Name).ToList();
            }
            else
            {
                if (sortOrder == "Ascending")
                    sortlist = customers.OrderBy(c => c.City).ToList();
                else
                    sortlist = customers.OrderByDescending(c => c.City).ToList();
            }
            return sortlist;
        }
    }
}
