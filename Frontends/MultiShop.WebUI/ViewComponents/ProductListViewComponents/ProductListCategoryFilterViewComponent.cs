using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Entities;
using MultiShop.DtoLayer.CatalogDtos.SubCategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.SubCategoryServices;
using System.Threading.Tasks;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class ProductListCategoryFilterViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public ProductListCategoryFilterViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }
    }
}
