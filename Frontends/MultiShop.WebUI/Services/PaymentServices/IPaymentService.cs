using MultiShop.DtoLayer.PaymentDtos;

namespace MultiShop.WebUI.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(CreatePaymentDto createPaymentDto);
    }
}
