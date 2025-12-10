using MultiShop.DtoLayer.PaymentDtos;

namespace MultiShop.WebUI.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<bool> CreatePaymentAsync(CreatePaymentDto createPaymentDto);
    }
}
