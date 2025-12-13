using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SubCategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.SubCategoryServices;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class ProductListSubCategoryFilterViewComponent : ViewComponent
    {
        private readonly ISubCategoryService _subCategoryService;

        public ProductListSubCategoryFilterViewComponent(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
                return View(new List<ResultSubCategoryDto>());
            }

            var values = await _subCategoryService.GetSubCategoryByCategoryIdAsync(categoryId);
            return View(values);
        }
    }
}
