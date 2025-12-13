using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.DiscountDtos;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Discount")]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            DiscountViewBagList();
            var values = await _discountService.GetAllCouponAsync();
            return View(values);
        }

        [Route("CreateDiscount")]
        [HttpGet]
        public IActionResult CreateDiscount()
        {
            DiscountViewBagList();
            return View();
        }

        [Route("CreateDiscount")]
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateCouponDto createCouponDto)
        {
            createCouponDto.CouponIsActive = true;
            createCouponDto.CouponValidDate = DateTime.Now.AddDays(3);
            await _discountService.CreateCouponAsync(createCouponDto);
            return RedirectToAction("Index", "Discount", new { area = "Admin" });
        }

        [Route("DeleteDiscount/{id}")]
        public async Task<IActionResult> DeleteDiscount(string id)
        {
            await _discountService.DeleteCouponAsync(id);
            return RedirectToAction("Index", "Discount", new { area = "Admin" });
        }

        [Route("UpdateDiscount/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateDiscount(string id)
        {
            DiscountViewBagList();
            var value = await _discountService.GetByIdCouponAsync(id);
            return View(value);
        }

        [Route("UpdateDiscount/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateDiscount(UpdateCouponDto updateCouponDto)
        {
            await _discountService.UpdateCouponAsync(updateCouponDto);
            return RedirectToAction("Index", "Discount", new { area = "Admin" });
        }

        void DiscountViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "İndirim Kuponu Tanımla";
            ViewBag.v3 = "İndirim Kuponu Listesi";
            ViewBag.v0 = "İndirim Kuponu İşlemleri";
        }
    }
}
