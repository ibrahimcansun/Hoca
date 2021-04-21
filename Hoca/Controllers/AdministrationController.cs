using Hoca.Models;
using Hoca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hoca.Controllers
{
    [Authorize(Policy = "AdminRolePolicy")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<OgrenciUser> userManager;
        private readonly ILogger<AdministrationController> logger;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<OgrenciUser> userManager,
                                        ILogger<AdministrationController> logger)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> YonetKullaniciClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"{userId} id'li kullanıcı bulunamadı.";
                return View("SayfaBulunamadi");
            }

            var existingUserClaims = await userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel
            {
                UserId = userId
            };

            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                if (existingUserClaims.Any(c => c.Type == claim.Type && c.Value == "true"))
                {
                    userClaim.IsSelected = true;
                }

                model.Cliams.Add(userClaim);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> YonetKullaniciClaims(UserClaimsViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if(user == null)
            {
                ViewBag.ErrorMessage = $"{model.UserId} id'li kullanıcı bulunamadı.";
                return View("SayfaBulunamadi");
            }

            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Claim silinemedi.");
                return View(model);
            }

            result = await userManager.AddClaimsAsync(user,
                model.Cliams.Select(c => new Claim(c.ClaimType, c.IsSelected ? "true" : "false")));
            
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Claim eklenmek için seçilmedi.");
                return View(model);
            }

            return RedirectToAction("DuzenleKullanici", new { Id = model.UserId});
        }


        [HttpGet]
        public async Task<IActionResult> KullaniciRolDuzenle(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"{userId} id'li kişi bulunamadı.";
                return View("SayfaBulunamadi");
            }

            var model = new List<UserRolesViewModel>();
            
            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciRolDuzenle(List<UserRolesViewModel> model ,string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if(user == null)
            {
                ViewBag.ErrorMessage = $"{userId} id'li kullanıcı bulunamadı";
                return View("SayfaBulunamadi");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Kullanıcı belirtilen rolden silinemedi.");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user, 
                model.Where(x => x.IsSelected).Select(y => y.RoleName));
            
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Eklemek için rol seçilmedi.");
                return View(model);
            }
            return RedirectToAction("DuzenleKullanici", new { Id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> SilKullanici(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"{id} id numaralı kişi bulunamadı.";
                return View("SayfaBulunamadi");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("KullanicilariListele");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("KullanicilariListele");
            }
        }

        [HttpPost]
        [Authorize(Policy = "RolSilPolicy")]
        public async Task<IActionResult> SilRol(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"{id} id numaralı rol bulunamadı.";
                return View("SayfaBulunamadi");
            }
            else
            {
                try
                {
                    var result = await roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("ListRoles");
                }
                catch (DbUpdateException ex)
                {
                    logger.LogError($"Rol silerken hata oldu {ex}");

                    ViewBag.ErrorTitle = $"{role.Name} rolü kullanımda.";
                    ViewBag.Error = $"{role.Name} rolüne sahip kullanıcılar bulunmakta"
                        + $". Önce kullanıcıları silin."
                        + $" Ardından rolü silmeyi tekrar deneyin.";
                    return View("SayfaBulunamadi");
                }
            }
        }

        [HttpGet]
        public IActionResult KullanicilariListele()
        {
            var kullanicilar = userManager.Users;
            return View(kullanicilar);
        }

        [HttpGet]
        public async Task<IActionResult> DuzenleKullanici(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if(user == null)
            {
                ViewBag.ErrorMessage = $"{id} id'li kullanıcı bulunamadı.";
                return View("SayfaBulunamadi");
            }

            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Class = user.Sinif,
                Claims = userClaims.Select(c => c.Type + " : " + c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DuzenleKullanici(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"{model.Id} id'li kullanıcı bulunamadı.";
                return View("SayfaBulunamadi");
            }
            else
            {
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Sinif = model.Class;

                var sonuc = await userManager.UpdateAsync(user);

                if (sonuc.Succeeded)
                {
                    return RedirectToAction("KullanicilariListele");
                }

                foreach (var error in sonuc.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult RolOlustur()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RolOlustur(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                
                IdentityResult sonuc = await roleManager.CreateAsync(identityRole);
                
                if (sonuc.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                foreach(IdentityError error in sonuc.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        [Authorize(Policy = "RolDuzenlePolicy")]
        public async Task<IActionResult> RolDuzenle(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"{id} numaralı bir sonuç bulunamadı";
                return View("SayfaBulunamadi");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach(var user in userManager.Users)
            {
                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }
        
        [HttpPost]
        [Authorize(Policy = "RolDuzenlePolicy")]
        public async Task<IActionResult> RolDuzenle(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"{model.Id} numaralı bir sonuç bulunamadı";
                return View("SayfaBulunamadi");
            }
            else
            {
                role.Name = model.RoleName;
                var sonuc = await roleManager.UpdateAsync(role);

                if (sonuc.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach(var error in sonuc.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> KullaniciDuzenle(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"{roleId} numarasına sahip kişi bulunamadı";
                return View("SayfaBulunamadi");
            }

            var model = new List<UserRoleViewModel>();

            foreach(var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email
                };

                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciDuzenle(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"{roleId} numaralı kişi bulunamadı";
                return View("SayfaBulunamadi");
            }

            for(int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("RolDuzenle", new { Id = roleId });
                    }
                }
            }

            return RedirectToAction("RolDuzenle", new { Id = roleId });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
