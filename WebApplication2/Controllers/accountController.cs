using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.view_model;

namespace WebApplication2.Controllers
{
    public class accountController : Controller
    {
        private readonly UserManager<applicationuser> userManager;
        private readonly SignInManager<applicationuser> signInManager;

        public accountController(UserManager<applicationuser> userManager,SignInManager<applicationuser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult regester()
        {
         
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> save(regestervm regestervm)
        {
            if(ModelState.IsValid)
            {
                applicationuser applicationuser = new applicationuser();
                applicationuser.address = regestervm.address;
                applicationuser.PhoneNumber = regestervm.phone;
                applicationuser.PasswordHash=regestervm.password;
                applicationuser.UserName = regestervm.name;

                IdentityResult result=  await userManager.CreateAsync(applicationuser,regestervm.password);
                if(result.Succeeded)
                {
                   await signInManager.SignInAsync(applicationuser,false);
                    return RedirectToAction("index", "product");
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("regester",regestervm);
        }

        public IActionResult log()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> login(logvm logvm)
        {
            if (ModelState.IsValid)
            {
                applicationuser applicationuser =
                    await userManager.FindByNameAsync(logvm.name);

                if(applicationuser != null)
                {
                   bool found =  await  userManager.CheckPasswordAsync(applicationuser, logvm.password);
                    if (found == true)
                    {
                       await signInManager.SignInAsync(applicationuser, logvm.remmber);
                        return RedirectToAction("index", "Home");
                    }
                }
                ModelState.AddModelError("", "not found password or name");
            }
            return View("log", logvm);
        }

        public async Task <IActionResult> logout()
        {
           await signInManager.SignOutAsync();
            return View ("log");
        }
    }
}
