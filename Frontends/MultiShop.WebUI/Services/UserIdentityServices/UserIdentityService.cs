using Microsoft.AspNetCore.Authentication;
using MultiShop.DtoLayer.IdentityDtos.UserDto;
using MultiShop.IdentityServer.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MultiShop.WebUI.Services.UserIdentityServices
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly HttpClient _httpClient;

        public UserIdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultUserDto>> GetAllUserListAsync()
        {
            var responseMessage = await _httpClient.GetAsync("https://localhost:5001/api/users/GetAllUserList");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);
            return values;
        }

        public async Task<ResultUserDto> GetUserDetailAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:5001/api/users/GetUserInfo");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultUserDto>(jsonData);
                return values;
            }
            return null;
        }

        public async Task UpdateUserDetailAsync(UpdateUserDto updateUserDto)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync("https://localhost:5001/api/users/UpdateUser", jsonContent);
        }
    }
}
