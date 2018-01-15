using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMShop.Models;

namespace DMShop.Controllers
{
    public class VendorController : Controller
    {
        DMShopContext OurContext = null;
        public VendorController(DMShopContext _OurContext)
        {
            OurContext = _OurContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddVendor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddVendor(Vendor Obj)
        {
            Obj.Date = DateTime.Now;
            OurContext.Vendor.Add(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(VendorController.ViewVendor));
        }
        public IActionResult ViewVendor()
        {
           
            return View(OurContext.Vendor.ToList<Vendor>());
        }
        public IActionResult Delete(int id)
        {
           Vendor Obj =  OurContext.Vendor.Where(abc => abc.Id == id).FirstOrDefault<Vendor>();
            OurContext.Vendor.Remove(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(VendorController.ViewVendor));
        }
        public IActionResult Details(int id)
        {
            Vendor Obj = OurContext.Vendor.Where(abc => abc.Id == id).FirstOrDefault<Vendor>();
            return View(Obj);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Vendor Obj = OurContext.Vendor.Where(abc => abc.Id == id).FirstOrDefault<Vendor>();
            
            return View(Obj);
        }
        [HttpPost]
        public IActionResult Edit(Vendor Obj)
        {
            OurContext.Vendor.Update(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(VendorController.ViewVendor));
        }
        public String CountVendor()
        {
            int a = OurContext.Vendor.ToList<Vendor>().Count();
            String count = a.ToString();
            return count;
        }
        public string VendorShowStar(int VendorId)
        {
            if (OurContext.Purchase.Where(m => m.VendorId == VendorId && m.Date >= DateTime.Now.AddDays(-4)).Count() > 0)
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