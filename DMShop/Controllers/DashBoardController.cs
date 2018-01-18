using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMShop.Models;

namespace DMShop.Controllers
{
    public class DashBoardController : Controller
    {
        DMShopContext OurContext = null;
        public DashBoardController(DMShopContext _OurContext)
        {
            OurContext = _OurContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public String UserCount()
        {
            int count = OurContext.User.ToList<User>().Count();
            string CategoryCount = "<h3>Total User(s) are = "+count+"</h3>";
            return CategoryCount;
        }
        public String ItemCount()
        {
            int count = OurContext.Item.ToList<Item>().Count();
            string ItemCount = "<h3>Total Item(s) are = " + count + "</h3>";
            return ItemCount;
        }
        public String CustomerCount()
        {
            int count = OurContext.Customer.ToList<Customer>().Count();
            string CustomerCount = "<h3>Total Customer(s) are = " + count + "</h3>";
            return CustomerCount;
        }
        public String VendorCount()
        {
            int count = OurContext.Vendor.ToList<Vendor>().Count();
            string VendorCount = "<h3>Total Vendor(s) are = " + count + "</h3>";
            return VendorCount;
        }
        public String PurchaseCount()
        {
            int count = OurContext.Purchase.ToList<Purchase>().Count();
            String PurchaseCount = "<h3>Total Purchase(s) are = " + count + "</h3>";
            return PurchaseCount;
        }
        public String SaleCount()
        {
            int count = OurContext.Sale.ToList<Sale>().Count();
            String SaleCount = "<h3>Total Sale(s) are = "+ count +" </h3>";
            return SaleCount;
        }
        //-------------------3 days Activity--------------------//
        public String ItemAddedCount()
        {
            int count = OurContext.Item.Where(m => m.Date >= DateTime.Now.AddDays(-4)).Count();
            return count.ToString();
        }
        public String SaleAddedCount()
        {
            int count = OurContext.Sale.Where(m => m.Date >= DateTime.Now.AddDays(-4)).Count();
            return count.ToString();
        }
        //public String PurchaseAddedCount()
        //{
        //    int count = OurContext.Purchase.Where(m => m.Date >= DateTime.Now.AddDays(-4)).Count();
        //    return count.ToString();
        //}
        //public String CustomerAddedCount()
        //{
        //    int count = OurContext.Customer.Where(m => m.Date >= DateTime.Now.AddDays(-4)).Count();
        //    return count.ToString();
        //}
        //public String VendorAddedCount()
        //{
        //    int count = OurContext.Vendor.Where(m => m.Date >= DateTime.Now.AddDays(-4)).Count();
        //    return count.ToString();
        //}
        public string LastAddedCategory()
        {
            try
            {
                int? dNumber = OurContext.Category.Where(db => db.Date >= DateTime.Now.AddDays(-3)).Max(u => (int?)u.Id);
                Category catName = OurContext.Category.Where(db => db.Id == (int)dNumber).SingleOrDefault();
                return catName.Name;
            }
            catch
            {
                return "Nill";
            }

        }
        
        //    public string MostUsedCategory()
        //{
        //    try
        //    {
        //        int? dNumber = OurContext.Sale.Where(db => db.Date >= DateTime.Now.AddDays(-3)).Max(u => (int?)u.ItemId);
        //        Item oItem = OurContext.Item.Where(m => m.Id == dNumber).FirstOrDefault<Item>();
        //        Category catName = OurContext.Category.Where(db => db.Id == oItem.CategoryId).SingleOrDefault();
        //        return catName.Name;
        //    }
        //    catch
        //    {
        //        return "Nill";
        //    }

        //}

        public String CategoryAddedCount()
        {
            int count = OurContext.Category.Where(m => m.Date >= DateTime.Now.AddDays(-4)).Count();
            return count.ToString();
        }
        
    }
}