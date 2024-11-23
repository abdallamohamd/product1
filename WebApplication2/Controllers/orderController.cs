using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using WebApplication2.Models;
using WebApplication2.view_model;

namespace WebApplication2.Controllers
{
    public class orderController : Controller
    {
        private readonly appcontext appcontext;

        public orderController(appcontext appcontext)
        {
            this.appcontext = appcontext;
        }

        public IActionResult Index()
        {
           List<order> orders =appcontext.orders.Include(x=>x.product).ToList();
            return View(orders);
        }

       public IActionResult create()
        {
            ViewData["plist"] = appcontext.products.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult save(ordervm ordervm)
        {
            if(ModelState.IsValid==true)
            {
                order order = new order();
                order.productid= ordervm.productid;
                order.qantity = ordervm.qantity;
                order.orderdate = DateTime.Now;

                product product = appcontext.products.FirstOrDefault(x => x.id == ordervm.productid);

                if (ordervm.qantity != null && ordervm.qantity < product.unitinstock)
                {
                    product.unitinstock -= ordervm.qantity;
                    order.Price = product.unitprice * ordervm.qantity;
                    appcontext.orders.Add(order);
                    appcontext.SaveChanges();
                    return RedirectToAction("index");
                }
            }

            ViewData["plist"] = appcontext.products.ToList();
            return View("create",ordervm);
        }

        public IActionResult edite(int id)
        {
            order order = appcontext.orders.FirstOrDefault(x => x.Id == id);
            return View(order);
        }

        [HttpPost]
        public IActionResult sedite(int id , order order)
        {
              if ( ModelState.IsValid == true)
               {
                order orderdb = appcontext.orders.FirstOrDefault(x => x.Id == id);


                product product = appcontext.products.FirstOrDefault(x=>x.id==orderdb.productid);


                int defferance =   order.qantity - orderdb.qantity;

                if ( order.qantity != null &&  order.qantity < product.unitinstock)
                {
                    product.unitinstock -= defferance;
                    orderdb.qantity = order.qantity;
                    orderdb.Price = order.qantity * product.unitprice;
                    appcontext.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            return View("edite",order);
        }

        public IActionResult delete(int id)
        {
            order order = appcontext.orders.FirstOrDefault(x => x.Id == id);
            if(order != null)
            {
                product product =appcontext.products.FirstOrDefault(x=>x.id == order.productid);
                product.unitinstock += order.qantity;
                appcontext.orders.Remove(order);
                appcontext.SaveChanges();
                return RedirectToAction("index");
            }
            return Content("not found id");
        }

    }
}
