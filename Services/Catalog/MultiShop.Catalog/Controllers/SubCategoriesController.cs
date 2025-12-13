using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.SubCategoryDtos;
using MultiShop.Catalog.Services.SubCategoryServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubCategory()
        {
            var values = await _subCategoryService.GetAllSubCategoryAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSubCategory(string id)
        {
            var value = await _subCategoryService.GetByIdSubCategoryAsync(id);
            return Ok(value);
        }

        [HttpGet("SubCategoryByCategory/{id}")]
        public async Task<IActionResult> GetSubCategoryByCategoryId(string id)
        {
            var values = await _subCategoryService.GetSubCategoryByCategoryIdAsync(id);
            return Ok(values);
        }

        [HttpGet("ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            await _subCategoryService.ChangeSubCategoryStatus(id);
            return Ok("Alt kategori durumu başarıyla değiştirildi.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(CreateSubCategoryDto createSubCategoryDto)
        {
            await _subCategoryService.CreateSubCategoryAsync(createSubCategoryDto);
            return Ok("Alt kategori ekleme işlemi başarılı.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubCategory(UpdateSubCategoryDto updateSubCategoryDto)
        {
            await _subCategoryService.UpdateSubCategoryAsync(updateSubCategoryDto);
            return Ok("Alt kategori güncelleme işlemi başarılı.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubCategory(string id)
        {
            await _subCategoryService.DeleteSubCategoryAsync(id);
            return Ok("Alt kategori silme işlemi başarılı.");
        }
    }
}
