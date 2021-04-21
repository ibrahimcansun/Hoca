using Hoca.Models;
using Hoca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Controllers
{
    public class SorularController : Controller
    {
        private readonly ISorularRepository _sorularRepository;
        private readonly UserManager<OgrenciUser> userManager;
        private readonly ILogger<SorularController> _logger;
        public SorularController(ISorularRepository sorularRepository,
                                UserManager<OgrenciUser> userManager,
                                ILogger<SorularController> logger)
        {
            _sorularRepository = sorularRepository;
            this.userManager = userManager;
            _logger = logger;
        }

        public ViewResult Index()
        {
            var model = _sorularRepository.GetAllSorular();
            return View(model);
        }

        public ViewResult Sorular(int? id)
        {
            //throw new Exception("Hata: Öğrenci gösteriminde hata oluştu.");

            //int soruId = Convert.ToInt32(protector.Unprotect(id));

            Sorular soru = _sorularRepository.GetSoru(id.Value);

            if (soru == null)
            {
                Response.StatusCode = 404;
                return View("OgrenciBulunamadi", id.Value);
            }

            SorularSorularViewModel sorularSorularViewModel = new SorularSorularViewModel()
            {
                Sorular = soru,
                PageTitle = "Sorular"
            };

            return View(sorularSorularViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Olustur(string OgrenciId)
        {
            SoruOlusturViewModel model = new SoruOlusturViewModel
            {
                OgrenciId = OgrenciId
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Olustur(SoruOlusturViewModel model)
        {
            if (ModelState.IsValid)
            {
                var kullanici = await userManager.FindByIdAsync(model.OgrenciId);
                
                if (kullanici == null)
                {
                    return View("SayfaBulunamadi");
                }

                Sorular newSoru = new Sorular
                {
                    SoruBaslik = model.SoruBaslik,
                    SoruIcerik = model.SoruIcerik,
                    SoruKategori = model.SoruKategori,
                    OgrenciId1 = kullanici.Id,
                    OgrenciAd = kullanici.UserName
                };

                _sorularRepository.Olustur(newSoru);
                return RedirectToAction("Sorular", new { id = newSoru.SoruID });
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ViewResult Duzenle(int id)
        {
            Sorular soru = _sorularRepository.GetSoru(id);
            SoruDuzenleViewModel sorularSorularViewModel = new SoruDuzenleViewModel
            {
                Id = soru.SoruID,
                SoruBaslik = soru.SoruBaslik,
                SoruIcerik = soru.SoruIcerik,
                SoruKategori = soru.SoruKategori
            };
            return View(sorularSorularViewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Duzenle(SoruDuzenleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Sorular soru = _sorularRepository.GetSoru(model.Id);
                soru.SoruBaslik = model.SoruBaslik;
                soru.SoruIcerik = model.SoruIcerik;
                soru.SoruKategori = model.SoruKategori;

                _sorularRepository.Guncelle(soru);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Sorduklarim(string OgrenciId)
        {

            //return View(_sorularRepository.GetOgrenciSorular(OgrenciId));
            return View();
        }

        public IActionResult Cevapla(int id)
        {

            return View();
        }

    }
}
