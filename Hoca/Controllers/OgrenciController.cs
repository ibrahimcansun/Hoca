using Hoca.Models;
using Hoca.Security;
using Hoca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;//IWebHostEnvironment
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly IOgrenciRepository _ogrenciRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ILogger<OgrenciController> _logger;
        private readonly IDataProtector protector;
        public OgrenciController(IOgrenciRepository ogrenciRepository,
                                IWebHostEnvironment hostingEnvironment,
                                ILogger<OgrenciController> logger,
                                IDataProtectionProvider dataProtectionProvider,
                                DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _ogrenciRepository = ogrenciRepository;
            this.hostingEnvironment = hostingEnvironment;
            _logger = logger;
            protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.OgrenciIdRouteValue);
        }

        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = _ogrenciRepository.GetAllOgrenci()
                        .Select(e =>
                        {
                            e.EncryptedId = protector.Protect(e.Id.ToString());
                            return e;
                        });
            return View(model);
        }

        public ViewResult Ogrenciler(string id)
        {
            //throw new Exception("Hata: Öğrenci gösteriminde hata oluştu.");

            int ogrenciId = Convert.ToInt32(protector.Unprotect(id));

            Ogrenci ogrenci = _ogrenciRepository.GetOgrenci(ogrenciId);
            if (ogrenci == null)
            {
                Response.StatusCode = 404;
                return View("OgrenciBulunamadi", ogrenciId);
            }
            HomeOgrencilerViewModel homeOgrencilerViewModel = new HomeOgrencilerViewModel()
            {
                Ogrenci = ogrenci,
                PageTitle = "Ogrenciler"
            };
            
            return View(homeOgrencilerViewModel);
        }

        [HttpGet]
        [Authorize]
        public ViewResult Olustur()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ViewResult Duzenle(int id)
        {
            Ogrenci ogr = _ogrenciRepository.GetOgrenci(id);
            OgrenciDuzenleViewModel ogrenciDuzenleViewModel = new OgrenciDuzenleViewModel
            {
                Id = ogr.Id,
                Ad = ogr.Ad,
                Soyad = ogr.Soyad,
                Email = ogr.Email,
                Sifre = ogr.Sifre,
                Sinif = ogr.Sinif,
                FotoPath = ogr.FotoPath
            };
            return View(ogrenciDuzenleViewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Duzenle(OgrenciDuzenleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Ogrenci ogrenci = _ogrenciRepository.GetOgrenci(model.Id);
                ogrenci.Ad = model.Ad;
                ogrenci.Soyad = model.Soyad;
                ogrenci.Email = model.Email;
                ogrenci.Sifre = model.Sifre;
                ogrenci.Sinif = model.Sinif;
                if (model.Foto != null) {
                    if(model.FotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, 
                            "images", model.FotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    ogrenci.FotoPath = FotoYukle(model);
                }

                _ogrenciRepository.Guncelle(ogrenci);
                return RedirectToAction("Index");
            }
            return View();
        }

        private string FotoYukle(OgrenciOlusturViewModel model)
        {
            string uniqFileName = null;
            if (model.Foto != null)
            {
                string yuklenenDosyalar = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqFileName = Guid.NewGuid().ToString() + "_" + model.Foto.FileName;
                string dosyaYolu = Path.Combine(yuklenenDosyalar, uniqFileName);
                using(var fileStream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    model.Foto.CopyTo(fileStream);
                }
            }

            return uniqFileName;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Olustur(OgrenciOlusturViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqFileName = FotoYukle(model);

                Ogrenci newOgrenci = new Ogrenci
                {
                    Ad = model.Ad,
                    Soyad = model.Soyad,
                    Email = model.Email,
                    Sifre = model.Sifre,
                    Sinif = model.Sinif,
                    FotoPath = uniqFileName
                };
                _ogrenciRepository.Olustur(newOgrenci);
                return RedirectToAction("Ogrenciler", new { id = newOgrenci.Id });
            }
            return View();
        }

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
