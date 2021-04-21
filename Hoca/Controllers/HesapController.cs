using Hoca.Models;
using Hoca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hoca.Controllers
{
    public class HesapController : Controller
    {
        private readonly UserManager<OgrenciUser> userManager;
        private readonly SignInManager<OgrenciUser> signInManager;
        private readonly ILogger<HesapController> logger;

        public HesapController(UserManager<OgrenciUser> userManager,
                                SignInManager<OgrenciUser> signInManager,
                                ILogger<HesapController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ProfilimiGoster(string id)
        {
            var kullanici = await userManager.FindByIdAsync(id);

            if(kullanici == null)
            {
                ViewBag.ErrorMessage = "Hesabınız bulunamadı.";
                return View("SayfaBulunamadi");
            }

            var model = new UserProfilEditViewModel
            {
                Id = kullanici.Id,
                UserName = kullanici.UserName,
                Class = kullanici.Sinif
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProfilimiGoster(UserProfilEditViewModel model)
        {
            var kullanici = await userManager.FindByIdAsync(model.Id);

            if(kullanici == null)
            {
                ViewBag.ErrorMessage = $"{model.Id} id'li hesap bulunamadı.";
                return View("SayfaBulunamadi");
            }
            else
            {
                kullanici.UserName = model.UserName;
                kullanici.Sinif = model.Class;

                var sonuc = await userManager.UpdateAsync(kullanici);

                if (sonuc.Succeeded)
                {
                    return RedirectToAction("ProfilimiGoster");
                }

                foreach (var error in sonuc.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ParolayiDegistir()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ParolayiDegistir(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var kullanici = await userManager.GetUserAsync(User);
                if (kullanici == null)
                {
                    return RedirectToAction("Giris");
                }

                var sonuc = await userManager.ChangePasswordAsync(kullanici,
                    model.CurrentPassword, model.NewPassword);

                if (!sonuc.Succeeded)
                {
                    foreach(var error in sonuc.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                await signInManager.RefreshSignInAsync(kullanici);
                return View("ParolayiDegistirBasarili");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ParolayiSifirla(string token, string email)
        {
            if(token == null || email == null)
            {
                ModelState.AddModelError(string.Empty, "Hatalı token.");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ParolayiSifirla(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var kullanici = await userManager.FindByEmailAsync(model.Email);
                if (kullanici != null)
                {
                    var sonuc = await userManager.ResetPasswordAsync(kullanici, model.Token, model.Sifre);
                    if (sonuc.Succeeded)
                    {
                        if (await userManager.IsLockedOutAsync(kullanici))
                        {//hesap kilitlendiyse kilidi kaldir
                            await userManager.SetLockoutEndDateAsync(kullanici, DateTimeOffset.UtcNow);
                        }

                        return View("ParolaSifirlandiOnayi");
                    }

                    foreach (var error in sonuc.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                return View("ParolaSifirlandiOnayi");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ParolamiUnuttum()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ParolamiUnuttum(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var kullanici = await userManager.FindByEmailAsync(model.Email);
                if (kullanici != null && await userManager.IsEmailConfirmedAsync(kullanici))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(kullanici);

                    var passwordResetLink = Url.Action("ParolayiSifirla", "Hesap",
                                    new { email = model.Email, token = token }, Request.Scheme);

                    logger.Log(LogLevel.Warning, passwordResetLink);

                    return View("ParolamiUnuttumOnayi");
                }
                return View("ParolamiUnuttumOnayi");
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Cikis()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Ogrenci");
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EmailDegistir(string id)
        {
            var kullanici = await userManager.FindByIdAsync(id);

            if (kullanici == null)
            {
                ViewBag.ErrorMessage = "Hesabınız bulunamadı.";
                return View("SayfaBulunamadi");
            }

            var model = new UserEmailEditViewModel
            {
                Email = kullanici.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EmailDegistir(UserEmailEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var kullanici = await userManager.FindByIdAsync(model.Id);

                if (kullanici == null)
                {
                    ViewBag.ErrorMessage("Kullanıcı bulunamadı.");
                    return View("SayfaBulunamadi");
                }
                else
                {
                    kullanici.Email = model.Email;

                    var sonuc = await userManager.UpdateAsync(kullanici);

                    if (sonuc.Succeeded)
                    {
                        return RedirectToAction("EmailDegistir");
                    }

                    foreach (var error in sonuc.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }

        //[HttpPost][HttpGet]
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var kullanici = await userManager.FindByEmailAsync(email);
            if(kullanici == null)
            {
                return Json(data: true);
            }
            else
            {
                return Json(data: $"{email} mail adresi kullanılıyor");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> KayitOl(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var kullanici = new OgrenciUser
                { 
                    UserName = model.Name,
                    Email = model.Email,
                    Sinif = model.Class
                };
                /*if (signInManager.IsSignedIn(User))
                {
                    return Redirect("KayitOl");
                }*/
                var sonuc = await userManager.CreateAsync(kullanici, model.Password);

                if (sonuc.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(kullanici);

                    var confirmationLink = Url.Action("ConfirmEmail", "Hesap",
                                        new { userId = kullanici.Id, token = token }, Request.Scheme);

                    logger.Log(LogLevel.Warning, confirmationLink);

                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("KullanicilariListele", "Administration");
                    }

                    ViewBag.ErrorTitle = "Kayıt tamamlandı";
                    ViewBag.ErrorMessage = "Giriş yapmadan önce lütfen mail adresinizi onaylayın." +
                        " Spam kutunuzu kontrol edin ve yollanan link üzerinden kaydınızı tamamlayın.";
                    return View("SayfaBulunamadi");
                }

                foreach(var error in sonuc.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Ogrenci");
            }

            var kullanici = await userManager.FindByIdAsync(userId);
            
            if(kullanici == null)
            {
                ViewBag.ErrorMessage = $"{userId} id'li kullanıcı bulunamadı.";
                return View("SayfaBulunamadi");
            }

            var sonuc = await userManager.ConfirmEmailAsync(kullanici, token);
            if (sonuc.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorTitle = "Mail adresi onaylanmadı";
            return View("SayfaBulunamadi");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Giris(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Giris(LoginViewModel model, string returnUrl)
        {
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            if (ModelState.IsValid)
            {
                var kullanici = await userManager.FindByEmailAsync(model.Email);

                if (kullanici != null && !kullanici.EmailConfirmed &&
                                (await userManager.CheckPasswordAsync(kullanici, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Mail adresinizi onaylayın!");
                    return View(model);
                }

                var sonuc = await signInManager.PasswordSignInAsync(model.Ad, 
                    model.Password, model.BeniHatirla, true);

                if (sonuc.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);//modelbinding
                    }
                    else
                    {
                        return RedirectToAction("Index", "Ogrenci");
                    }
                }

                if (sonuc.IsLockedOut)
                {
                    return View("HesapKilitlendi");
                }


                ModelState.AddModelError(string.Empty, "Hatalı Giriş Yaptınız");
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallBack", "Hesap",
                            new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl=null,string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Hatanın alındığı yer: {remoteError}");

                return View("Giris", loginViewModel);//Google girisi yapilmazsa gel buraya
            }

            var info = await signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                ModelState.AddModelError(string.Empty, $"Giriş sırasında hata yaşandı");
                return View("Giris", loginViewModel);
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            OgrenciUser user = null;

            if (email != null)
            {
                user = await userManager.FindByEmailAsync(email);

                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Mail adresinizi onaylayın");
                    return View("Giris", loginViewModel);
                }
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                if (email != null)
                {
                    if(user == null)
                    {
                        user = new OgrenciUser
                        {//sadece name olabilir givenname yerine
                            UserName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };
                        await userManager.CreateAsync(user);

                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmationLink = Url.Action("ConfirmEmail", "Hesap",
                                    new { userId = user.Id, token = token }, Request.Scheme);

                        logger.Log(LogLevel.Warning, confirmationLink);

                        ViewBag.ErrorTitle = "Kayıt başarılı";
                        ViewBag.ErrorMessage = "Giriş yapmadan önce lütfen mail adresinizi onaylayın." +
                            " Mail adresinize yollanan link üzerinden onaylayabilirsiniz.";
                        return View("SayfaBulunamadi");
                    }

                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorTitle = $"Mail onayı gelmedi: {info.LoginProvider}";
                ViewBag.ErrorMessage = $"Lütfen teknik destek ekibimiz ile bağlantıya geçin.";
                return View("SayfaBulunamadi");
            }
        }
    }
}
