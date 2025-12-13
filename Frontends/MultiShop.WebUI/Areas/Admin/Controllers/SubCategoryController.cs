using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.SubCategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.SubCategoryServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/SubCategory")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;

        public SubCategoryController(ISubCategoryService subCategoryService, ICategoryService categoryService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            SubCategoryViewbagList();
            var values = await _subCategoryService.GetAllSubCategoryAsync();
            return View(values);
        }

        [Route("ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            await _subCategoryService.ChangeSubCategoryStatus(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("CreateSubCategory")]
        public async Task<IActionResult> CreateSubCategory()
        {
            SubCategoryViewbagList();

            var values = await _categoryService.GetAllCategoryAsync();
            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId
                                                   }).ToList();

            ViewBag.categories = categoryValues;
            return View();
        }

        [HttpPost]
        [Route("CreateSubCategory")]
        public async Task<IActionResult> CreateSubCategory(CreateSubCategoryDto createSubCategoryDto)
        {

            await _subCategoryService.CreateSubCategoryAsync(createSubCategoryDto);
            return RedirectToAction("Index", "SubCategory", new { area = "Admin" });
        }

        [Route("DeleteSubCategory/{id}")]
        public async Task<IActionResult> DeleteSubCategory(string id)
        {
            await _subCategoryService.DeleteSubCategoryAsync(id);
            return RedirectToAction("Index", "SubCategory", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateSubCategory/{id}")]
        public async Task<IActionResult> UpdateSubCategory(string id)
        {
            SubCategoryViewbagList();

            var values = await _categoryService.GetAllCategoryAsync();
            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId
                                                   }).ToList();

            ViewBag.categories = categoryValues;
            var value = await _subCategoryService.GetByIdSubCategoryAsync(id);
            return View(value);
        }

        [HttpPost]
        [Route("UpdateSubCategory/{id}")]
        public async Task<IActionResult> UpdateSubCategory(UpdateSubCategoryDto updateSubCategoryDto)
        {
            await _subCategoryService.UpdateSubCategoryAsync(updateSubCategoryDto);
            return RedirectToAction("Index", "SubCategory", new { area = "Admin" });
        }

        void SubCategoryViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Alt Kategoriler";
            ViewBag.v3 = "Alt Kategori Listesi";
            ViewBag.v0 = "Alt Kategori İşlemleri";
        }
    }
}
