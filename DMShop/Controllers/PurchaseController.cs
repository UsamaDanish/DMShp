using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMShop.Controllers
{
    public class PurchaseController : Controller
    {
        DMShopContext OurContext=null;
        public PurchaseController(DMShopContext _OurContext)
        {
            OurContext = _OurContext;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddPurchase()
        {
            ViewBag.VendorId = new SelectList(OurContext.Vendor, "Id", "Name");
            ViewBag.ItemId = new SelectList(OurContext.Item, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddPurchase(Purchase Obj)
        {
           
            Item oItem=OurContext.Item.Where(abc => abc.Id == Obj.ItemId).FirstOrDefault<Item>();
            oItem.Quantity = oItem.Quantity + Obj.Quantity;
            OurContext.Item.Update(oItem);
            OurContext.Purchase.Add(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(PurchaseController.ViewPurchase));
        }
        public IActionResult ViewPurchase()
        {
            return View(OurContext.Purchase.ToList<Purchase>());
        }
        public IActionResult Delete(int id)
        {
            Purchase Obj = OurContext.Purchase.Where(abc => abc.Id == id).FirstOrDefault<Purchase>();

            OurContext.Purchase.Remove(Obj);
            Item oItem = OurContext.Item.Where(abc => abc.Id == Obj.ItemId).FirstOrDefault<Item>();
            oItem.Quantity = oItem.Quantity - Obj.Quantity;
            OurContext.SaveChanges();
            return RedirectToAction(nameof(PurchaseController.ViewPurchase));
        }
        public IActionResult Details(int id)
        {
            Purchase Obj = OurContext.Purchase.Where(abc => abc.Id == id).FirstOrDefault<Purchase>();
            return View(Obj);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.VendorId = new SelectList(OurContext.Vendor, "Id", "Name");
            ViewBag.ItemId = new SelectList(OurContext.Item, "Id", "Name");
            Purchase Obj = OurContext.Purchase.Where(abc => abc.Id == id).FirstOrDefault();
            GlobalVar.PurchaseItemQuantity = Obj.Quantity;
            return View(Obj);
        }
        [HttpPost]
        public IActionResult Edit(Purchase oPurchase)
        {
            Item oItem = OurContext.Item.Where(abc => abc.Id == oPurchase.ItemId).FirstOrDefault<Item>();
            int OpeningBalance=oItem.Quantity - GlobalVar.PurchaseItemQuantity;
            oItem.Quantity=OpeningBalance + oPurchase.Quantity;
            OurContext.Item.Update(oItem);
            OurContext.Purchase.Update(oPurchase);
            OurContext.SaveChanges();

            return View();
        }
        public IActionResult PurchaseHistory()
        {
            
            return View(OurContext.Purchase.ToList<Purchase>());
        }
        public String CountPurchase()
        {
            int a = OurContext.Purchase.ToList<Purchase>().Count();
            string count = a.ToString();
            return count;
        }
       
        
    }
}