using MultiShop.DtoLayer.CatalogDtos.SubCategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.SubCategoryServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.SubSubCategoryServices
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly HttpClient _httpClient;

        public SubCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ChangeSubCategoryStatus(string id)
        {
            var responseMessage = await _httpClient.GetAsync("subcategories/ChangeStatus/" + id);
        }

        public async Task CreateSubCategoryAsync(CreateSubCategoryDto createSubCategoryDto)
        {
            createSubCategoryDto.SubCategoryStatus = true;
            await _httpClient.PostAsJsonAsync<CreateSubCategoryDto>("subcategories", createSubCategoryDto);
        }

        public async Task DeleteSubCategoryAsync(string id)
        {
            await _httpClient.DeleteAsync("subcategories?id=" + id);
        }

        public async Task<List<ResultSubCategoryDto>> GetAllSubCategoryAsync()
        {
            var responseMessage = await _httpClient.GetAsync("subcategories");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultSubCategoryDto>>(jsonData);
            return values;
        }

        public async Task<UpdateSubCategoryDto> GetByIdSubCategoryAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("subcategories/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<UpdateSubCategoryDto>();
            return value;
        }

        public async Task<List<ResultSubCategoryDto>> GetSubCategoryByCategoryIdAsync(string categoryId)
        {
            var responseMessage = await _httpClient.GetAsync("subcategories/subcategorybycategory/" + categoryId);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadFromJsonAsync<List<ResultSubCategoryDto>>();
                return jsonData;
            }
            return null;
        }

        public async Task UpdateSubCategoryAsync(UpdateSubCategoryDto updateSubCategoryDto)
        {
            updateSubCategoryDto.SubCategoryStatus = true;
            await _httpClient.PutAsJsonAsync<UpdateSubCategoryDto>("subcategories", updateSubCategoryDto);
        }
    }
}
