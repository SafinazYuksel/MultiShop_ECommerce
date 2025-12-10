using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;
using System.Net.NetworkInformation;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class DefaultFeatureProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public DefaultFeatureProductViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _productService.GetTop8ProductAsync();
            return View(values);
        }
    }
}
