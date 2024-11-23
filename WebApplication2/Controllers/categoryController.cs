using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class categoryController : Controller
    {
        private readonly appcontext appcontext;

        public categoryController(appcontext appcontext)
        {
            this.appcontext = appcontext;
        }

        public IActionResult Index()
        {
            List<category> categories = appcontext.categories.ToList();
            return View(categories);
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult screate(category category)
        {
            if(ModelState.IsValid)
            {
                appcontext.categories.Add(category);
                appcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("create", category);
        }
        public IActionResult edite (int id)
        {
            category category =appcontext.categories.Find(id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult sedite (int id ,category category)
        {
            if (ModelState.IsValid)
            {
                category categorydb =appcontext.categories.Find(id);
                categorydb.name= category.name;
                categorydb.description= category.description;
                appcontext.SaveChanges();
                return RedirectToAction ("Index");
            }
            return View("edite",category);
        }
        public IActionResult delete (int id)
        {
           category category = appcontext.categories.Find(id);
            if(category != null)
            {
                appcontext.categories.Remove(category);
                appcontext.SaveChanges();
                return RedirectToAction("index");
            }
            return Content("not found");
        }
    }
}
