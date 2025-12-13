using MultiShop.DtoLayer.DiscountDtos;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public interface IDiscountService
    {
        Task<GetDiscountCodeDetailByCodeDto> GetDiscountCode(string code);
        Task<int> GetDiscountCouponCountRate(string code);

        Task<UpdateCouponDto> GetByIdCouponAsync(string id);
        Task<List<ResultCouponDto>> GetAllCouponAsync();
        Task CreateCouponAsync(CreateCouponDto createCouponDto);
        Task UpdateCouponAsync(UpdateCouponDto updateCouponDto);
        Task DeleteCouponAsync(string id);
        Task<ResultCouponDto> GetCodeDetailByCodeAsync(string code);
        Task<int> GetDiscountCouponCount();
    }
}
