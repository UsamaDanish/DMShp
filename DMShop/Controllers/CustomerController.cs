using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMShop.Models;

namespace DMShop.Controllers
{
    public class CustomerController : Controller
    {
        DMShopContext OurContext = null;
        public CustomerController(DMShopContext _OurContext)
        {
            OurContext = _OurContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();

        }
        [HttpPost]
        public IActionResult AddCustomer(Customer Obj)
        {
            Obj.Date = DateTime.Now;
            OurContext.Customer.Add(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(CustomerController.ViewCustomer));

        }
        public IActionResult ViewCustomer()
        {
            return View(OurContext.Customer.ToList<Customer>());
        }
        public IActionResult Delete(int id)
        {
            Customer Obj = OurContext.Customer.Where(abc => abc.Id == id).FirstOrDefault<Customer>();
            OurContext.Customer.Remove(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(CustomerController.ViewCustomer));
        }
        public IActionResult Details(int id)
        {
            Customer Obj = OurContext.Customer.Where(abc => abc.Id == id).FirstOrDefault<Customer>();
            return View(Obj);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Customer Obj = OurContext.Customer.Where(abc => abc.Id == id).FirstOrDefault<Customer>();
            return View(Obj);
        }
        [HttpPost]
        public IActionResult Edit(Customer Obj)
        {
            Obj.Date = DateTime.Now;
            OurContext.Customer.Update(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(CustomerController.ViewCustomer));
        }
        public String CountCustomer()
        {
            int a = OurContext.Customer.ToList<Customer>().Count();
            string count = a.ToString();
            return count;
        }
        public string CustomerShowStar(int CustomerId)
        {
           
            if (OurContext.Sale.Where(m => m.CustomerId == CustomerId && m.Date >= DateTime.Now.AddDays(-28)).Count()>0)
            {
                return "star";
            }
            else
            {
                return "";
            }
        }
    }
}