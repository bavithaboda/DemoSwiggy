using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SupportEngineersController> _logger;
        public SupportEngineersController(ISupportEmp supportRepo,ILogger<SupportEngineersController> logger)
        {
            _supportRepo = supportRepo;
            _logger = logger;
        }

        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("name");
            sortModel.AddColumn("city");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;

            _logger.LogInformation($"Fetching the details from Index :: sortModel{sortModel} SearchText:{SearchText} SortExpression:{sortExpression}");

            PaginatedList<Customer> customer = _supportRepo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            var pager = new PageModel(customer.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;


            TempData["CurrentPage"]= pg;


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
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (customer.Description.Length < 4 || customer.Description == null)
                {
                    errMessage = "Customer description must be atleast 4 characters";
                    
                }

                if (_supportRepo.IsCustomerNameExists(customer.Name)== true)
                {
                    errMessage = errMessage+""+"Customer Name "+customer.Name+" Exists Already";
                }
                if (errMessage == "")
                {
                    customer = _supportRepo.Create(customer);
                        bolret = true;
                }

                //customer = _supportRepo.Create(customer);
            }
            catch(Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
                _logger.LogError($"Exception Occurred :: {nameof(SupportEngineersController)}::{nameof(Create)}::{ex.Message}");
                
            }

            if (bolret == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(customer);
            }
            else 
            {
                TempData["SuccessMessage"] = "Customer " + customer.Name + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            
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
            bool bolret = false;
            string errMessage = "";

            try
            {
                if (customer.Description.Length < 4 || customer.Description == null)
                    errMessage = "Customer description must be atleast 4 characters";

                if (_supportRepo.IsCustomerNameExists(customer.Name) == true)
                {
                    errMessage = errMessage + "" + "Customer Name " + customer.Name + " Exists Already";
                }
                if (errMessage == "")
                {
                    customer = _supportRepo.Edit(customer);
                    bolret = true;
                }

            }
            catch(Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
                _logger.LogError($"Exception Occurred :: {nameof(SupportEngineersController)}::{nameof(Edit)}::{ex.Message}");

            }


            TempData["SuccessMessage"] = "Customer "+ customer.Name + " Saved Successfully";

            int CurrentPage = 1;
            if (TempData["CurrentPage"] != null)
                CurrentPage = (int)TempData["CurrentPage"];


            if (bolret == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(customer);
            }
            else
            {
                TempData["SuccessMessage"] = "Customer " + customer.Name + " Created Successfully";
                return RedirectToAction(nameof(Index));
            }


            return RedirectToAction(nameof(Index),new { pg=CurrentPage});
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
            catch(Exception ex)
            {
                _logger.LogError($"Exception Occurred :: {nameof(SupportEngineersController)}::{nameof(Delete)}::{ex.Message}");
            }

            TempData["SuccessMessage"] = "Customer "+customer.Name + " Deleted Successfully";
            int CurrentPage = 1;
            if (TempData["CurrentPage"] != null)
                CurrentPage = (int)TempData["CurrentPage"];


            return RedirectToAction(nameof(Index), new { pg = CurrentPage });
        }
    }
    
}
