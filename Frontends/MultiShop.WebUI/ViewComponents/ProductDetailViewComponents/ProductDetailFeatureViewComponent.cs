using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class ProductDetailFeatureViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductDetailFeatureViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new UpdateProductDto());
            }

            try
            {
                var values = await _productService.GetByIdProductAsync(id);

                if (values == null)
                {
                    return View(new UpdateProductDto());
                }

                return View(values);
            }
            catch (System.Exception)
            {
                return View(new UpdateProductDto());
            }
        }
    }
}
