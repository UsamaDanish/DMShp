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
    public class SaleController : Controller
    {
        DMShopContext OurContext=null;
        public SaleController(DMShopContext _OurContext)
        {
            OurContext = _OurContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddSale()
        {
            ViewData["ItemId"] = new SelectList(OurContext.Item, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(OurContext.Customer, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddSale(Sale Obj)
        {
            
            OurContext.Sale.Add(Obj);
            Item oItem = OurContext.Item.Where(abc => abc.Id == Obj.ItemId).FirstOrDefault<Item>();
            oItem.Quantity = oItem.Quantity - Obj.Quantity;
            OurContext.Update(oItem);
            OurContext.SaveChanges();
            //sendMail(Obj);
            return RedirectToAction(nameof(SaleController.AddSale));
        }
        public IActionResult ViewSale()
        {
            return View(OurContext.Sale.ToList<Sale>());
        }
        public IActionResult Delete(int id)
        {
            Sale Obj = OurContext.Sale.Where(abc => abc.Id == id).FirstOrDefault<Sale>();
            Item oItem = OurContext.Item.Where(abc => abc.Id == Obj.ItemId).FirstOrDefault<Item>();
            oItem.Quantity = oItem.Quantity + Obj.Quantity;
            OurContext.Update(oItem);
            OurContext.Sale.Remove(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(SaleController.ViewSale));
        }
        public IActionResult Details(int id)
        {
            Sale Obj = OurContext.Sale.Where(abc => abc.Id == id).FirstOrDefault<Sale>();
            return View(Obj);
        }
        public IActionResult SaleHistory()
        {
            return View(OurContext.Sale.ToList<Sale>());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Sale Obj = OurContext.Sale.Where(abc => abc.Id == id).FirstOrDefault<Sale>();
            GlobalVar.SaleItemQuantity = Obj.Quantity;
            return View(Obj);
        }
        [HttpPost]
        public IActionResult Edit(Sale Obj)
        {
            Item oItem=OurContext.Item.Where(abc => abc.Id == Obj.ItemId).FirstOrDefault<Item>();
            int ItemQuantity = oItem.Quantity + GlobalVar.SaleItemQuantity;
            oItem.Quantity = ItemQuantity + Obj.Quantity;
            OurContext.Item.Update(oItem);
            OurContext.Sale.Update(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(SaleController.ViewSale));
        }
        public void sendMail(Sale Obj)
        {
            try
            {
                //setting up Mail Message
                MailMessage oMail = new MailMessage();
                oMail.Subject = "These are Greetings of Sale";
                oMail.Body = "Dear Customer,<br><br> you have bought " + Obj.ItemId + " with worth of " + Obj.TotalPrice + "." +
                    "I hope you will enjoy this " + Obj.ItemId + ".For Any Complaint and Suggestion Please Contact with YourSelf ";
                oMail.IsBodyHtml = true;
                oMail.To.Add(new MailAddress("usama.danish2233@gmail.com"));
                oMail.From=new MailAddress("usamadanish22@gmail.com", "MobileShopFlow");
                //setting up SMTP Client
                SmtpClient oSmtp = new SmtpClient();
                oSmtp.Port = 587;
                oSmtp.EnableSsl = true;
                oSmtp.Host = "smtp.gmail.com";
                //Giving Creditientails to Mails
                oSmtp.Credentials = new System.Net.NetworkCredential("usamadanish22@gmail.com", "@Gmail@12");
                oSmtp.Send(oMail);
            }
            catch(Exception ex)
            {

            }
        }
        public string CountSale()
        {
            int a = OurContext.Sale.ToList<Sale>().Count();
            string count = a.ToString();
            return count;
        }
    }
}