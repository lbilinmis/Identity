using Identity.CoreWebUI.Context;
using Identity.CoreWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.CoreWebUI.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public PanelController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewModelAppUser model = new ViewModelAppUser
            {
                Email = user.Email,
                Ad = user.Ad,
                PhoneNumber = user.PhoneNumber,
                Soyad = user.Soyad,
                ImageUrl = user.ImageUrl
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUser(ViewModelAppUser model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uygulamaninCalistigiYer = Directory.GetCurrentDirectory();
                    string dosyaUzantisi = Path.GetExtension(model.Image.FileName);
                    var resimAdi = Guid.NewGuid() + dosyaUzantisi;

                    string kaydetmeYeri = uygulamaninCalistigiYer + "/wwwroot/images/" + resimAdi;

                    var stream = new FileStream(kaydetmeYeri, FileMode.Create);

                    await model.Image.CopyToAsync(stream);
                    user.ImageUrl = resimAdi;
                }

                user.Ad = model.Ad;
                user.Soyad = model.Soyad;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("UserLogin","Home");
        }

    }
}
