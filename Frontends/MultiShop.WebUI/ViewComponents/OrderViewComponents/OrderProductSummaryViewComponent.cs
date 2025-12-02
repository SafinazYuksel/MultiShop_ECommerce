using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace MultiShop.WebUI.ViewComponents.OrderViewComponents
{
    public class OrderProductSummaryViewComponent : ViewComponent
    {
        private readonly IBasketService _basketService;

        public OrderProductSummaryViewComponent(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var getBasket = await _basketService.GetBasket();
            ViewBag.total = getBasket.TotalPrice;
            var totalPriceWithTax = getBasket.TotalPrice + 49 +  getBasket.TotalPrice / 100 * 10;
            var tax = getBasket.TotalPrice / 100 * 10;
            ViewBag.totalPriceWithTax = totalPriceWithTax;
            ViewBag.tax = tax;

            var basketTotal = await _basketService.GetBasket();
            var basketItems = basketTotal.BasketItems;
            return View(basketItems);
        }
    }
}
