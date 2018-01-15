using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMShop.Models;
using System.Net.Mail;

namespace DMShop.Controllers
{
    public class LoginController : Controller
    {
        DMShopContext OurContext = null;
        public LoginController(DMShopContext _OurContext)
        {
            OurContext = _OurContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewUser()
        {
            return View(OurContext.User.ToList<User>());
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(User Obj)
        {
            Obj.Date = DateTime.Now;
            OurContext.User.Add(Obj);
            sendMail(Obj);
            OurContext.SaveChanges();
           
            return View();
        }
       [HttpGet]
        public IActionResult NewLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewLogin(User Obj)
        {
            User RObj = OurContext.User.Where(abc => abc.Name == Obj.Name).FirstOrDefault<User>();
            if(RObj != null)
            {
                if (RObj.Password == Obj.Password)
                {
                    return RedirectToAction("Index" , "DashBoard");
                }
                else
                {
                    ViewBag.ErrorMessage = "Incorrect Password";
                    return View();
                }
            }
           else
            {
                ViewBag.ErrorMessage = "User Name is Incorrect";
                return View();
            }
        }
       public IActionResult Delete(int id)
        {
            User Obj = OurContext.User.Where(abc => abc.Id == id).FirstOrDefault<User>();
            OurContext.User.Remove(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(LoginController.ViewUser));
        }
        public IActionResult Details(int id)
        {
            return View(OurContext.User.Where(abc => abc.Id == id).FirstOrDefault<User>());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(OurContext.User.Where(abc => abc.Id == id).FirstOrDefault<User>());
        }
        [HttpPost]
        public IActionResult Edit(User Obj)
        {
            OurContext.User.Update(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(LoginController.ViewUser));
        }
        public IActionResult Login()
        {
            return View();
        }
        
        public string CountUsers()
        {
            int a = OurContext.User.ToList<User>().Count();
            string count = a.ToString();
            return count;
        }
        public IActionResult checkLoginUsingForm(string username, string password)
        {
            User RObj = OurContext.User.Where(abc => abc.Name == username).FirstOrDefault<User>();
            if (RObj != null)
            {
                if (RObj.Password == password)
                {
                    return RedirectToAction("Index", "DashBoard");
                }
                else
                {
                    return View();

                }
            }
            else
            {
                return View();
            }
        }

        public void sendMail(User Obj)
        {
            try
            {
                //setting up Mail Message
                MailMessage oMail = new MailMessage();
                oMail.Subject = "Notification About New Added "+Obj.Role+" .";
                oMail.Body = "Dear,<br><br> A new person Named "+Obj.Name+" have joined Us with Authority of" +
                    ""+Obj.Role+" . ";
                oMail.IsBodyHtml = true;
                IList<User> AllUser = OurContext.User.ToList<User>();
                foreach(var item in AllUser)
                {
                    oMail.To.Add(new MailAddress(""+item.Email+""));
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
                
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Mail can not be sent due to " + ex + "";
            }
        }
    }
}