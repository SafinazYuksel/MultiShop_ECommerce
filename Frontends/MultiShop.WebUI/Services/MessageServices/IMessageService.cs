using MultiShop.DtoLayer.MessageDtos;

namespace MultiShop.WebUI.Services.MessageServices
{
    public interface IMessageService
    {
        Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string receiverId);
        Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string senderId);
        Task<int> GetTotalMessageCountByReceiverId(string id);
    }
}
