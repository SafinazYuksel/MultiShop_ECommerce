using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class ProductDetailImageSliderViewComponent : ViewComponent
    {
        private readonly IProductImageService _productImageService;

        public ProductDetailImageSliderViewComponent(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new UpdateProductImageDto());
            }

            try
            {
                var values = await _productImageService.GetByIdProductImageAsync(id);

                if (values == null)
                {
                    return View(new UpdateProductImageDto());
                }

                return View(values);
            }
            catch (System.Exception)
            {
                return View(new UpdateProductImageDto());
            }
        }
    }
}
