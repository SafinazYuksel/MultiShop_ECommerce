using MultiShop.DtoLayer.PaymentDtos;

namespace MultiShop.WebUI.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            var response = await _httpClient.PostAsJsonAsync("payment", createPaymentDto);
            return response.IsSuccessStatusCode;
        }
    }
}
