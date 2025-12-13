using AutoMapper;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using MultiShop.Mail.Context;
using MultiShop.Mail.Dtos;
using MultiShop.Mail.Entities;

namespace MultiShop.Mail.Services
{
    public class MailService : IMailService
    {
        private readonly MailContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public MailService(MailContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task CreateSentMailAsync(CreateSentMailDto createSentMailDto)
        {
            await _context.SentMails.AddAsync(_mapper.Map<SentMail>(createSentMailDto));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSentMailAsync(string id)
        {
            var value = await _context.SentMails.FindAsync(id);
            _context.Remove(value);
            await _context.SaveChangesAsync();
        }

        public List<ResultInboxMailDto> GetInboxMails()
        {
            var values = new List<ResultInboxMailDto>();

            try
            {
                using (var client = new ImapClient())
                {
                    var host = _configuration["MailSettings:ImapHost"];
                    var port = int.Parse(_configuration["MailSettings:ImapPort"]);
                    var username = _configuration["MailSettings:Mail"];
                    var password = _configuration["MailSettings:Password"];

                    client.Connect(host, port, true);
                    client.Authenticate(username, password);

                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);

                    int count = inbox.Count;
                    int limit = 20;

                    for (int i = count - 1; i >= count - limit && i >= 0; i--)
                    {
                        var message = inbox.GetMessage(i);
                        values.Add(new ResultInboxMailDto
                        {
                            MessageId = message.MessageId,
                            SenderName = message.From.Count > 0 ? message.From[0].Name : "Bilinmiyor",
                            SenderMail = message.From.Count > 0 ? ((MailboxAddress)message.From[0]).Address : "Yok",
                            Subject = message.Subject,
                            Date = message.Date.DateTime,
                            Body = !string.IsNullOrEmpty(message.TextBody) ? message.TextBody : "HTML İçerik"
                        });
                    }
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                // Loglama yapılabilir
            }
            return values;
        }

        public ResultInboxMailDto GetMailDetail(string messageId)
        {
            using (var client = new ImapClient())
            {
                var host = _configuration["MailSettings:ImapHost"];
                var port = int.Parse(_configuration["MailSettings:ImapPort"]);
                var username = _configuration["MailSettings:Mail"];
                var password = _configuration["MailSettings:Password"];

                client.Connect(host, port, true);
                client.Authenticate(username, password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                var results = inbox.Search(SearchQuery.HeaderContains("Message-Id", messageId));

                if (results.Count > 0)
                {
                    var message = inbox.GetMessage(results[0]);
                    client.Disconnect(true);

                    return new ResultInboxMailDto
                    {
                        MessageId = message.MessageId,
                        SenderName = message.From.Count > 0 ? message.From[0].Name : "Bilinmiyor",
                        SenderMail = message.From.Count > 0 ? ((MailboxAddress)message.From[0]).Address : "Yok",
                        Subject = message.Subject,
                        Date = message.Date.DateTime,
                        Body = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody
                    };
                }

                client.Disconnect(true);
                return null;
            }
        }

        public List<ResultInboxMailDto> GetSentMails()
        {
            var values = new List<ResultInboxMailDto>();

            try
            {
                using (var client = new ImapClient())
                {
                    var host = _configuration["MailSettings:ImapHost"];
                    var port = int.Parse(_configuration["MailSettings:ImapPort"]);
                    var username = _configuration["MailSettings:Mail"];
                    var password = _configuration["MailSettings:Password"];

                    client.Connect(host, port, true);
                    client.Authenticate(username, password);

                    var sentFolder = client.GetFolder(SpecialFolder.Sent);

                    if (sentFolder == null)
                        sentFolder = client.GetFolder("[Gmail]/Sent Mail");

                    sentFolder.Open(FolderAccess.ReadOnly);

                    int count = sentFolder.Count;
                    int limit = 20;

                    for (int i = count - 1; i >= count - limit && i >= 0; i--)
                    {
                        var message = sentFolder.GetMessage(i);
                        values.Add(new ResultInboxMailDto
                        {
                            MessageId = message.MessageId,
                            SenderName = message.To.Count > 0 ? message.To[0].Name : "Alıcı Yok",
                            SenderMail = message.To.Count > 0 ? ((MailboxAddress)message.To[0]).Address : "Yok",
                            Subject = message.Subject,
                            Date = message.Date.DateTime,
                            Body = !string.IsNullOrEmpty(message.TextBody) ? message.TextBody : "İçerik Yüklenemedi"
                        });
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
            }
            return values;
        }

        public async Task UpdateSentMailAsync(UpdateSentMailDto updateSentMailDto)
        {
            _context.SentMails.Update(_mapper.Map<SentMail>(updateSentMailDto));
            await _context.SaveChangesAsync();
        }
    }
}
