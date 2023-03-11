using Identity.CoreWebUI.Context;
using Identity.CoreWebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Identity.CoreWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult UserLogin()
        {
            return View(new ViewModelLogInUser());
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UserLogin(ViewModelLogInUser loginModel)
        {
            if (ModelState.IsValid)
            {
                var loginUser = await _userManager.FindByNameAsync(loginModel.UserName);
                if (loginUser==null)
                {
                    ModelState.AddModelError("", "Kullanıcı adı ve ya da şifre hatalı");

                    return View(loginModel);
                }
                var signInResult = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, loginModel.RememberMe, true);

                if (signInResult.IsLockedOut)
                {
                    var sure = await _userManager.GetLockoutEndDateAsync(loginUser);
                    var kisitlamaSuresi = sure.Value;
                    var kalanDakika =  kisitlamaSuresi.Minute - DateTime.Now.Minute;

                    ModelState.AddModelError("", "5 kez hatalı giriş yaptığınız için hesabınız geçici süre ile kilitlenmiştir");
                    ModelState.AddModelError("", "Kalan Süre : "+kalanDakika+" dk");
                    return View(loginModel);
                }

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }

                var hataliGiris = await _userManager.GetAccessFailedCountAsync(loginUser);
                ModelState.AddModelError("", $"Kullanıcı adı ya da şifre hatalı  {5-hataliGiris} defa yanlış girilmesi durumunda hesabınınz geçici süre ile kilitlenecektir.");
            }
            return View(loginModel);
        }

        public IActionResult UserRegister()
        {
            return View(new ViewModelRegisterUser());
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(ViewModelRegisterUser model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.Email = model.Email;
                user.Ad = model.Ad;
                user.Soyad = model.Soyad;
                user.UserName = model.UserName;
                user.ImageUrl = "";

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserLogin");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}