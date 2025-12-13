using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Dtos.SubCategoryDtos;

namespace MultiShop.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<GetByIdProductDto> GetByIdProductAsync(string id);
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task<List<ResultProductDto>> GetTop8ProductAsync();
        Task<List<ResultProductDto>> GetAllProductSortByPriceAsync(string? sortType);
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(string id);
        Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync();
        Task<List<ResultProductDto>> GetProductsWithByCategoryIdAsync(string categoryId);
        Task<List<ResultProductDto>> GetProductsBySubCategoryAsync(string subCategoryId);
    }
}
