using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class supplierController : Controller
    {
        private readonly appcontext appcontext;

        public supplierController(appcontext appcontext)
        {
            this.appcontext = appcontext;
        }

        public IActionResult Index()
        {
            List <supplier> suppliers = appcontext.suppliers.ToList();
            return View(suppliers);
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult screate(supplier supplier)
        {
            if(ModelState.IsValid)
            {
                appcontext.suppliers.Add(supplier);
                appcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("create",supplier);
        }

        public IActionResult edite(int id )
        {
         supplier supplier = appcontext.suppliers.FirstOrDefault(x=>x.id==id);
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult sedite(int id,supplier supplier)
        {
            supplier supplierdb = appcontext.suppliers.FirstOrDefault(x => x.id == id);
            if (ModelState.IsValid)
            {
                supplierdb.name=supplier.name; 
                supplierdb.country=supplier.country;
                appcontext.SaveChanges();
                return RedirectToAction("index");
            }
            return View("edite ", supplier);
        }
        public IActionResult delete (int id)
        {
            supplier supplier = appcontext.suppliers.FirstOrDefault(x => x.id == id);
            if(supplier != null)
            {
                appcontext.suppliers.Remove(supplier);
                appcontext.SaveChanges();
                return RedirectToAction("index");
            }
            return Content("id not fount ");
        }

    }
}
