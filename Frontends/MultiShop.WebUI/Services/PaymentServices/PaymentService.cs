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

        public async Task CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7076/api/Payments/", createPaymentDto);
        }
    }
}
