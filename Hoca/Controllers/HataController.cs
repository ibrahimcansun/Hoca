using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Controllers
{
    [AllowAnonymous]
    public class HataController : Controller
    {
        /*private readonly ILogger<HataController> logger;

        public HataController(ILogger<HataController> logger)
        {
            this.logger = logger;
        }
        */
        [Route("Hata/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Gitmek istediğiniz sayfa bulunamadı";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    /*logger.LogWarning($"Hata meydana geldi. Path = {statusCodeResult.OriginalPath}" +
                        $" ve QueryString = {statusCodeResult.OriginalQueryString}");*/
                    break;
            }
            return View("SayfaBulunamadi");
        }

        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrace = exceptionDetails.Error.StackTrace;
            /*logger.LogError($"Path: {exceptionDetails.Path} hata yolladı" + 
                $"{exceptionDetails.Error}");*/
            
            return View("Error");
        }
    }
}
