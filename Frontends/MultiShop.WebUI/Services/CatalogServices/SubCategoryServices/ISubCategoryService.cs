
using MultiShop.DtoLayer.CatalogDtos.SubCategoryDtos;

namespace MultiShop.WebUI.Services.CatalogServices.SubCategoryServices
{
    public interface ISubCategoryService
    {
        Task<UpdateSubCategoryDto> GetByIdSubCategoryAsync(string id);
        Task<List<ResultSubCategoryDto>> GetAllSubCategoryAsync();
        Task<List<ResultSubCategoryDto>> GetSubCategoryByCategoryIdAsync(string categoryId);
        Task CreateSubCategoryAsync(CreateSubCategoryDto createSubCategoryDto);
        Task UpdateSubCategoryAsync(UpdateSubCategoryDto updateSubCategoryDto);
        Task DeleteSubCategoryAsync(string id);
        Task ChangeSubCategoryStatus(string id);
    }
}
