using Microsoft.EntityFrameworkCore;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            await _httpClient.PostAsJsonAsync<CreateProductDto>("products", createProductDto);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _httpClient.DeleteAsync("products?id=" + id);
        }

        public async Task<List<ResultProductDto>> GetAllProductSortByPriceAsync(string? sortType)
        {
            string requestUrl = "products";

            if (!string.IsNullOrEmpty(sortType))
            {
                requestUrl += $"?sort={sortType}";
            }

            var responseMessage = await _httpClient.GetAsync(requestUrl);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return values;
            }

            return new List<ResultProductDto>();
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var responseMessage = await _httpClient.GetAsync("products");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            return value;
        }

        public async Task<UpdateProductDto> GetByIdProductAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("products/" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
            return value;
        }

        public async Task<List<ResultProductDto>> GetProductsWithByCategoryIdAsync(string categoryId)
        {
            var responseMessage = await _httpClient.GetAsync("products/GetProductsWithByCategoryId?id=" + categoryId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            return values;
        }

        public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var responseMessage = await _httpClient.GetAsync("products/GetProductsWithCategory");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductsWithCategoryDto>>(jsonData);
            return values;
        }

        public async Task<List<ResultProductDto>> GetTop8ProductAsync()
        {
            var responseMessage = await _httpClient.GetAsync("products/GetTop8Product");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            return values;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductDto>("products", updateProductDto);
        }

        public async Task<List<ResultProductDto>> GetProductsBySubCategoryAsync(string subCategoryId)
        {
            var responseMessage = await _httpClient.GetAsync("products/GetProductsBySubCategoryAsync?id=" + subCategoryId);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>();
                return jsonData;
            }
            return new List<ResultProductDto>();
        }
    }
}
