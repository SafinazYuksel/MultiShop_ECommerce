using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Mail.Dtos;
using MultiShop.Mail.Services;

namespace MultiShop.Mail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentMailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public SentMailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpGet("GetSentBox")]
        public IActionResult GetSentBox()
        {
            var values = _mailService.GetSentMails();
            return Ok(values);
        }

        [HttpGet("GetInbox")]
        public IActionResult GetInbox()
        {
            var values = _mailService.GetInboxMails();
            return Ok(values);
        }

        [HttpGet("GetMailDetail/{id}")]
        public IActionResult GetMailDetail(string id)
        {
            var value = _mailService.GetMailDetail(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSentMail(CreateSentMailDto createSentMailDto)
        {
            await _mailService.CreateSentMailAsync(createSentMailDto);
            return Ok("Mail gönderme işlemi başarılı.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSentMail(UpdateSentMailDto updateSentMailDto)
        {
            await _mailService.UpdateSentMailAsync(updateSentMailDto);
            return Ok("Mail güncelleme başarılı.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSentMail(string id)
        {
            await _mailService.DeleteSentMailAsync(id);
            return Ok("Mail gönderilemedi.");
        }
    }
}
