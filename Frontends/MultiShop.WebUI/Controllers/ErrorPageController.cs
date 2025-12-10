using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ErrorPageController : Controller
    {
        [Route("ErrorPage/Index/{code}")]
        public IActionResult Index(int code)
        {
            switch (code)
            {
                case 404:
                    ViewBag.ErrorMessage = "Aradığınız sayfa bulunamadı.";
                    return View("Error404");

                case 401:
                    ViewBag.ErrorMessage = "Bu sayfaya erişim yetkiniz yok.";
                    return View("Error401");

                case 403:
                    ViewBag.ErrorMessage = "Bu işlem için yetkiniz kısıtlı.";
                    return View("Error403");

                case 500:
                    ViewBag.ErrorMessage = "Sunucuda bir hata oluştu. Teknik ekibimiz ilgileniyor.";
                    return View("Error500");

                default:
                    return View("ErrorGeneral");
            }
        }
    }
}
