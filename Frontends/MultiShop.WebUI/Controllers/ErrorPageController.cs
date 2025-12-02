using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ErrorPageController : Controller
    {
        [Route("ErrorPage/Error401")]
        public IActionResult Error401()
        {
            return View();
        }

        [Route("ErrorPage/Error404")]
        public IActionResult Error404()
        {
            return View();
        }

        [Route("ErrorPage/Error500")]
        public IActionResult Error500()
        {
            return View();
        }
    }
}
