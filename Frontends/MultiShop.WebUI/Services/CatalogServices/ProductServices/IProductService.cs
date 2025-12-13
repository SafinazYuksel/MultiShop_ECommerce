using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public interface IProductService
    {
        Task<UpdateProductDto> GetByIdProductAsync(string id);
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
