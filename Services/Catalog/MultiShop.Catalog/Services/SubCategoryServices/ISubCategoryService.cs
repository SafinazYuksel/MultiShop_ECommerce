using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.SubCategoryDtos;

namespace MultiShop.Catalog.Services.SubCategoryServices
{
    public interface ISubCategoryService
    {
        Task<GetByIdSubCategoryDto> GetByIdSubCategoryAsync(string id);
        Task<List<ResultSubCategoryDto>> GetAllSubCategoryAsync();
        Task<List<ResultSubCategoryDto>> GetSubCategoryByCategoryIdAsync(string categoryId);
        Task CreateSubCategoryAsync(CreateSubCategoryDto createSubCategoryDto);
        Task UpdateSubCategoryAsync(UpdateSubCategoryDto updateSubCategoryDto);
        Task DeleteSubCategoryAsync(string id);
        Task ChangeSubCategoryStatus(string id);
    }
}
