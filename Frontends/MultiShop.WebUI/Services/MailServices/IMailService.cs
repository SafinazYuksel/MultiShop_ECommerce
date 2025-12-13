using MultiShop.DtoLayer.MailDtos;

namespace MultiShop.WebUI.Services.MailServices
{
    public interface IMailService
    {
        Task<List<ResultInboxMailDto>> GetInboxMailAsync();
        Task<List<ResultInboxMailDto>> GetSentMailListAsync();
        Task<ResultInboxMailDto> GetMailDetail(string messageId);
        Task CreateMailAsync(CreateSentMailDto createSentMailDto);
    }
}
