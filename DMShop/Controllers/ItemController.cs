using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMShop.Controllers
{
    public class ItemController : Controller
    {
        
        DMShopContext OurContext = null;
        IHostingEnvironment env = null;

        public ItemController(DMShopContext _OurContext , IHostingEnvironment _env)
        {
            env = _env;
            OurContext = _OurContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddItem()
        {
            ViewBag.CategoryID = new SelectList(OurContext.Category, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddItem(Item Obj , ICollection<IFormFile> Image)
        {
            String Root = env.WebRootPath;
            String ItemImage = Root + "/ItemImages/";
            foreach(var image in Image)
            {
                String FileName = image.FileName;
                String FileNameWithOutExtension = Path.GetFileNameWithoutExtension(FileName);
                String Extenstion = Path.GetExtension(FileName);
                FileStream fs = new FileStream(ItemImage + FileNameWithOutExtension + Extenstion, FileMode.CreateNew);
                image.CopyTo(fs);
                fs.Close();
                fs.Dispose();
                Obj.Image = ItemImage + FileNameWithOutExtension + Extenstion;
            }

            
            OurContext.Item.Add(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(ItemController.ViewItems));
        }
        public IActionResult ViewItems()
        {
            return View(OurContext.Item.ToList<Item>());
        }
        public IActionResult Delete(int id)
        {
            Item Obj = OurContext.Item.Where(abc => abc.Id == id).FirstOrDefault<Item>();
            IList<Purchase> iPurchase = OurContext.Purchase.Where(abc => abc.ItemId == Obj.Id).ToList<Purchase>();
            IList<Sale> iSale = OurContext.Sale.Where(abc => abc.ItemId == Obj.Id).ToList<Sale>();
            if(iPurchase.Count>0)
            {
                OurContext.Purchase.RemoveRange(iPurchase);
            }
            if(iSale.Count>0)
            {
                OurContext.Sale.RemoveRange(iSale);
            }   
            OurContext.Item.Remove(Obj);
            OurContext.SaveChanges();
            return RedirectToAction(nameof(ItemController.ViewItems));
        }
        public IActionResult Details(int id)
        {
            Item Obj = OurContext.Item.Where(abc => abc.Id == id).FirstOrDefault<Item>();
            return View(Obj);
        }
        public IActionResult ViewPannel()
        {
            return View(OurContext.Item.ToList<Item>());
        }
        public String CountItem()
        {
            int a = OurContext.Item.ToList<Item>().Count();
            string count = a.ToString();
            return count;
        }
    }
}