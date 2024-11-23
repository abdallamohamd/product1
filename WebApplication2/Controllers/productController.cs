using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class productController : Controller
    {
        private readonly appcontext appcontext;

        public productController(appcontext appcontext)
        {
            this.appcontext = appcontext;
        }

        public IActionResult Index(string cate)
        {
            List<product> products = appcontext.products.Include(x => x.supplier).Include(x => x.category).ToList();
            if (!string.IsNullOrEmpty(cate))
            {
               products = products.Where(x=>x.name.Contains(cate)).ToList();
           }
            return View(products);
        }
        public IActionResult Create()
        {
            ViewData["clist"] = appcontext.categories.ToList();
            ViewData["slist"] = appcontext.suppliers.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> save(IFormFile imageFile,product product)
        {
            if(imageFile !=null && imageFile.Length>0 )
            {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine("wwwroot/imge", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    product.pic = fileName;

                    appcontext.products.Add(product);
                    await appcontext.SaveChangesAsync();
                    return RedirectToAction("Index");
            }
            ViewData["clist"] = appcontext.categories.ToList();
            ViewData["slist"] = appcontext.suppliers.ToList();
            return View("create", product);

        }

        public IActionResult edite(int id)
        {
           product product= appcontext.products.FirstOrDefault(x=>x.id == id);

            ViewData["clist"] = appcontext.categories.ToList();
            ViewData["slist"] = appcontext.suppliers.ToList();

            
            
            return View(product);
        }

        private void Product_quantityover()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> sedite (int id ,IFormFile imageFile,product product)
        {
           product productdb = appcontext.products.FirstOrDefault(x => x.id == id);
           

            if (imageFile != null && imageFile.Length > 0)
            {
                var oldFilePath = Path.Combine("wwwroot/imge/", productdb.pic);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine("wwwroot/imge", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                productdb.name = product.name;
                productdb.unitprice=product.unitprice;
                productdb.categoryid = product.categoryid;
                productdb.supplierid=product.supplierid;
                productdb.pic = fileName;
                await appcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["clist"] = appcontext.categories.ToList();
            ViewData["slist"] = appcontext.suppliers.ToList();
            return View("edite", product);
        }

        public IActionResult delete(int id)
        {
            product product= appcontext.products.FirstOrDefault(x => x.id == id);

            if (product != null)
            {
                var oldFilePath = Path.Combine("wwwroot/imge/", product.pic);
                if (System.IO.File.Exists(oldFilePath))
                {
                  System.IO.File.Delete(oldFilePath);
                }
                
                appcontext.products.Remove(product);
                appcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return Content("not found");

        }

    }
}
