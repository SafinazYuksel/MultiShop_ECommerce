using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;
using MultiShop.DtoLayer.PaymentDtos;
using MultiShop.WebUI.Services.Abstract;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.PaymentServices;

namespace MultiShop.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
        private readonly IBasketService _basketService;

        public PaymentController(IUserService userService, IPaymentService paymentService, IBasketService basketService)
        {
            _userService = userService;
            _paymentService = paymentService;
            _basketService = basketService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Sepetim";
            ViewBag.directory3 = "Ödeme Ekranı";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreatePaymentDto createPaymentDto)
        {
            var user = await _userService.GetUserInfo();

            createPaymentDto.UserId = user.Id;
            createPaymentDto.OrderId = Guid.NewGuid().ToString();

            await _paymentService.CreatePaymentAsync(createPaymentDto);
            await _basketService.DeleteBasket(user.Id);

            return RedirectToAction("PaymentSuccess");
        }

        [HttpGet]
        public IActionResult PaymentSuccess()
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Sepetim";
            ViewBag.directory3 = "Ödeme Tamamlandı";

            return View();
        }
    }
}
