using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMShop.Models;

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
            OurContext.User.Add(Obj);
            OurContext.SaveChanges();

            return View();
        }
       [HttpGet]
       public IActionResult CheckLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckLogin(User Obj)
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
                    return View();
                   
                }
            }
           else
            {
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
        public IActionResult NewLogin()
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

    }
}