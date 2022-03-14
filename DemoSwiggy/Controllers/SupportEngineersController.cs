using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSwiggy.Interfaces;
using DemoSwiggy.Tools;
using DemoSwiggy.Models;
using DemoSwiggy.Repositories;

using Microsoft.EntityFrameworkCore;
namespace DemoSwiggy.Controllers
{
    public class SupportEngineersController : Controller
    {
        private readonly ISupportEmp _supportRepo;
        public SupportEngineersController(ISupportEmp supportRepo)
        {
            _supportRepo = supportRepo;
        }

        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("name");
            sortModel.AddColumn("city");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;
            PaginatedList<Customer> customer = _supportRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);
            var pager = new PageModel(customer.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;            
            return View(customer);
        }
        public IActionResult Create()
        {
            Customer customer = new Customer();
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            try
            {
                customer = _supportRepo.Create(customer);
            }
            catch
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            Customer customer = _supportRepo.GetCustomer(id);
            return View(customer);
        }
        public IActionResult Edit(int id)
        {
            Customer customer = _supportRepo.GetCustomer(id);
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            try
            {
                customer = _supportRepo.Edit(customer);
            }
            catch
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Customer customer = _supportRepo.GetCustomer(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            try
            {
                customer = _supportRepo.Delete(customer);
            }
            catch
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
