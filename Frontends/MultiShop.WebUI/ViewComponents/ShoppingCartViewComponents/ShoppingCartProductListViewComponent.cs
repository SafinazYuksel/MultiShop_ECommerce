using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.WebUI.Services.BasketServices;

namespace MultiShop.WebUI.ViewComponents.ShoppingCartViewComponents
{
    public class ShoppingCartProductListViewComponent : ViewComponent
    {
        private readonly IBasketService _basketService;

        public ShoppingCartProductListViewComponent(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basketTotal = await _basketService.GetBasket();
            var basketItems = basketTotal?.BasketItems ?? new List<BasketItemDto>();
            return View(basketItems);
        }
    }
}
