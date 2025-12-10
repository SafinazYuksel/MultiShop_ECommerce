using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class ProductListPriceFilterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string sort)
        {
            return View();
        }
    }
}
