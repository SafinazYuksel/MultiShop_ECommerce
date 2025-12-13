using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.SubCategoryDtos;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id);
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(string id);
        Task ChangeCategoryStatus(string id);
    }
}
