using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class ProductDetailInformationViewComponent : ViewComponent
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailInformationViewComponent(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new UpdateProductDetailDto());
            }

            try
            {
                var values = await _productDetailService.GetByIdProductDetailAsync(id);

                if (values == null)
                {
                    return View(new UpdateProductDetailDto());
                }

                return View(values);
            }
            catch (System.Exception)
            {
                return View(new UpdateProductDetailDto());
            }
        }
    }
}
