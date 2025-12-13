using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Entities;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class ProductListProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductListProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id, string subCategoryId)
        {
            if (!string.IsNullOrEmpty(subCategoryId))
            {
                var values = await _productService.GetProductsBySubCategoryAsync(subCategoryId);
                return View(values);
            }

            if (!string.IsNullOrEmpty(id))
            {
                var values = await _productService.GetProductsWithByCategoryIdAsync(id);
                return View(values);
            }

            var allValues = await _productService.GetAllProductAsync();
            return View(allValues);
        }
    }
}
