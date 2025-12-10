using Microsoft.CodeAnalysis.CSharp.Syntax;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.WebUI.Services.Abstract;
using Newtonsoft.Json;
using System.Text.Json;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly ILoginService _loginService;

        public BasketService(HttpClient httpClient, ILoginService loginService)
        {
            _httpClient = httpClient;
            _loginService = loginService;
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            var responseMessage = await _httpClient.GetAsync("baskets");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(jsonData))
            {
                return null;
            }
            
            var values = JsonConvert.DeserializeObject<BasketTotalDto>(jsonData);
            return values;
        }

        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {
            var values = await GetBasket();

            if (values == null)
            {
                values = new BasketTotalDto();
                values.UserId = _loginService.GetUserId;
                values.DiscountCode = "";
                values.DiscountRate = 0;
                values.BasketItems = new List<BasketItemDto>();
            }

            var existingItem = values.BasketItems.FirstOrDefault(x => x.ProductId == basketItemDto.ProductId);

            if (existingItem == null)
            {
                values.BasketItems.Add(basketItemDto);
            }
            else
            {
                existingItem.Quantity += basketItemDto.Quantity;
            }

            await SaveBasket(values);
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values = await GetBasket();
            if (values == null) return false;

            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            if (deletedItem == null) return false;

            var result = values.BasketItems.Remove(deletedItem);
            await SaveBasket(values);
            return true;
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await _httpClient.PostAsJsonAsync<BasketTotalDto>("baskets", basketTotalDto);
        }

        public async Task DeleteBasket(string userId)
        {
            await _httpClient.DeleteAsync($"baskets/DeleteBasket/{userId}");
        }
    }
}
