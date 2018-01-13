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
        public String CategoryCount()
        {
            int count = OurContext.Category.ToList<Category>().Count();
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
    }
}