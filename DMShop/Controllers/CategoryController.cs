using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMShop.Models;
using System.Net.Mail;


namespace DMShop.Controllers
{
    public class CategoryController : Controller
    {
        DMShopContext OurContext = null;
        public CategoryController(DMShopContext _OurContext)
        {
            OurContext = _OurContext;

        }
        
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category Obj)
        {
            Obj.Date = DateTime.Now;
            if(OurContext.Category.Where(m => m.Name == Obj.Name).Count()>0)
            {
                ViewBag.Exists = "*This Category Already Exists";
                return View();
            }
            OurContext.Category.Add(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(CategoryController.ViewCategories));
        }
        public IActionResult ViewCategories()
        {
           
            return View(OurContext.Category.ToList<Category>());
        }
        public IActionResult DeleteCategory(int id)
        {
            
            Category Obj = OurContext.Category.Where(abc => abc.Id == id).FirstOrDefault<Category>();
            IList<Item> iItem = OurContext.Item.Where(abc => abc.CategoryId == Obj.Id).ToList<Item>();
            OurContext.Category.Remove(Obj);
            if(iItem.Count>0)
            {
            OurContext.Item.RemoveRange(iItem);
            }
            OurContext.SaveChanges();
            return RedirectToAction(nameof(CategoryController.ViewCategories));
        }
        public IActionResult Details(int id)
        {
            Category Obj = OurContext.Category.Where(abc => abc.Id == id).FirstOrDefault<Category>();
            return View(Obj);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category Obj = OurContext.Category.Where(abc => abc.Id == id).FirstOrDefault();
            return View(Obj);
        }
        [HttpPost]
        public IActionResult Edit(Category Obj)
        {
            Obj.Date = DateTime.Now;
            OurContext.Category.Update(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(CategoryController.ViewCategories));
        }
        public int CountItems(int CategoryId)
        {
            return OurContext.Item.Where(m => m.CategoryId == CategoryId).Count();
        }
    }
}