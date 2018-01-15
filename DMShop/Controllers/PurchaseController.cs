using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mail;

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
            sendMail(Obj);
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
        public void sendMail(Purchase Obj)
        {
            try
            {
                User Admin = OurContext.User.Where(m => m.Role == "Admin").FirstOrDefault<User>();
                //setting up Mail Message
                MailMessage oMail = new MailMessage();
                oMail.Subject = "Notifing about new purchase";
                oMail.Body = "Respected Admin,<br><br> We have purchased " + Obj.ItemId + " with worth of " + Obj.TotalPrice + "From Our Vendor" + Obj.VendorId + " .";
                oMail.IsBodyHtml = true;
                oMail.To.Add(new MailAddress(Admin.Email));
                oMail.From = new MailAddress("usamadanish22@gmail.com", "MobileShopFlow");
                //setting up SMTP Client
                SmtpClient oSmtp = new SmtpClient();
                oSmtp.Port = 587;
                oSmtp.EnableSsl = true;
                oSmtp.Host = "smtp.gmail.com";
                //Giving Creditientails to Mails
                oSmtp.Credentials = new System.Net.NetworkCredential("usamadanish22@gmail.com", "@Gmail@12");
                oSmtp.Send(oMail);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorInMail = "Mail could not be sent because of" + ex + "";
            }
        }
        


    }
}