using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;
using MultiShop.DtoLayer.PaymentDtos;
using MultiShop.WebUI.Services.Abstract;
using MultiShop.WebUI.Services.PaymentServices;

namespace MultiShop.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;

        public PaymentController(IUserService userService, IPaymentService paymentService)
        {
            _userService = userService;
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Sepetim";
            ViewBag.directory3 = "Ödeme Ekranı";

            return View();
        }

        public async Task<IActionResult> Index(CreatePaymentDto createPaymentDto)
        {
            var values = await _userService.GetUserInfo();
            createPaymentDto.UserId = values.Id;
            createPaymentDto.OrderId = Guid.NewGuid().ToString();

            var result = await _paymentService.CreatePaymentAsync(createPaymentDto);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(createPaymentDto);
        }
    }
}
