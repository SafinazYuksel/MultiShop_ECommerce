using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class ProductListGetAllViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductListGetAllViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? sort)
        {
            var values = await _productService.GetAllProductSortByPriceAsync(sort);
            return View(values);
        }
    }
}
