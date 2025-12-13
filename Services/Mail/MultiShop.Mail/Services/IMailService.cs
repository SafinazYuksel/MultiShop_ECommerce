using MultiShop.Mail.Dtos;

namespace MultiShop.Mail.Services
{
    public interface IMailService
    {
        List<ResultInboxMailDto> GetSentMails();
        List<ResultInboxMailDto> GetInboxMails();
        ResultInboxMailDto GetMailDetail(string messageId);
        Task CreateSentMailAsync(CreateSentMailDto createSentMailDto);
        Task UpdateSentMailAsync(UpdateSentMailDto updateSentMailDto);
        Task DeleteSentMailAsync(string id);
    }
}
