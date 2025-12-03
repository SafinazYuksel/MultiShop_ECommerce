using MultiShop.DtoLayer.MessageDtos;

namespace MultiShop.WebUI.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string receiverId)
        {
            var responseMessage = await _httpClient.GetAsync("Messages/GetInboxMessage/" + receiverId);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultInboxMessageDto>>();
            return values;
        }

        public async Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string senderId)
        {
            var responseMessage = await _httpClient.GetAsync("Messages/GetSendboxMessage/" + senderId);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSendboxMessageDto>>();
            return values;
        }

        public async Task<int> GetTotalMessageCountByReceiverId(string id)
        {
            var responseMessage = await _httpClient.GetAsync("Messages/GetTotalMessageCountByReceiverId/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<int>();
            return value;
        }
    }
}
