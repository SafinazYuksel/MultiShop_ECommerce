using MailKit;
using MailKit.Net.Imap;
using Microsoft.AspNetCore.Authentication;
using MimeKit;
using MultiShop.DtoLayer.MailDtos;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Services.MailServices
{
    public class MailService : IMailService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MailService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateMailAsync(CreateSentMailDto createSentMailDto)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:5000/services/mail/sentmail", createSentMailDto);
        }

        public async Task<List<ResultSentMailDto>> GetAllMailListAsync()
        {
            var responseMessage = await _httpClient.GetAsync("https://localhost:5000/services/mail/sentmail");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSentMailDto>>(jsonData);
                return values;
            }

            return new List<ResultSentMailDto>();
        }

        public async Task<List<ResultInboxMailDto>> GetInboxMailAsync()
        {
            var responseMessage = await _httpClient.GetAsync("sentmail/GetInbox");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultInboxMailDto>>(jsonData);
                return values;
            }
            return new List<ResultInboxMailDto>();
        }

        public async Task<ResultInboxMailDto> GetMailDetail(string messageId)
        {
            var responseMessage = await _httpClient.GetAsync("sentmail/GetMailDetail/" + messageId);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultInboxMailDto>(jsonData);
                return values;
            }
            return new ResultInboxMailDto();
        }

        public async Task<List<ResultInboxMailDto>> GetSentMailListAsync()
        {
            var responseMessage = await _httpClient.GetAsync("sentmail/GetSentBox");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultInboxMailDto>>(jsonData);
                return values;
            }
            return new List<ResultInboxMailDto>();
        }
    }
}
