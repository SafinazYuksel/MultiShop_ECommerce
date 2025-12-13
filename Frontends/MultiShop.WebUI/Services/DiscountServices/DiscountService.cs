using MultiShop.DtoLayer.DiscountDtos;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCouponAsync(CreateCouponDto createCouponDto)
        {
            await _httpClient.PostAsJsonAsync<CreateCouponDto>("discounts", createCouponDto);
        }

        public async Task DeleteCouponAsync(string id)
        {
            await _httpClient.DeleteAsync("discounts?id=" + id);
        }

        public async Task<List<ResultCouponDto>> GetAllCouponAsync()
        {
            var responseMessage = await _httpClient.GetAsync("discounts");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCouponDto>>(jsonData);
            return values;
        }

        public async Task<UpdateCouponDto> GetByIdCouponAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("discounts/" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<UpdateCouponDto>(jsonData);
            return value;
        }

        public Task<ResultCouponDto> GetCodeDetailByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<GetDiscountCodeDetailByCodeDto> GetDiscountCode(string code)
        {
            var responseMessage = await _httpClient.GetAsync("https://localhost:7071/api/Discounts/GetCodeDetailByCode?code=" + code);
            var values = await responseMessage.Content.ReadFromJsonAsync<GetDiscountCodeDetailByCodeDto>();
            return values;
        }

        public Task<int> GetDiscountCouponCount()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetDiscountCouponCountRate(string code)
        {
            var responseMessage = await _httpClient.GetAsync("https://localhost:7071/api/Discounts/GetCouponCountRate?code=" + code);
            var values = await responseMessage.Content.ReadFromJsonAsync<int>();
            return values;
        }

        public async Task UpdateCouponAsync(UpdateCouponDto updateCouponDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateCouponDto>("discounts", updateCouponDto);
        }
    }
}
