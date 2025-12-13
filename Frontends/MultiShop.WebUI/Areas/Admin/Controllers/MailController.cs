using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MultiShop.DtoLayer.MailDtos;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.MailServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Mail")]
    public class MailController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public MailController(IConfiguration configuration, IMailService mailService)
        {
            _configuration = configuration;
            _mailService = mailService;
        }

        [Route("SentBox")]
        [HttpGet]
        public async Task<IActionResult> SentBox()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Mailler";
            ViewBag.v3 = "Mail Listesi";
            ViewBag.v0 = "Mail İşlemleri";

            var values = await _mailService.GetSentMailListAsync();
            return View(values);
        }

        [Route("Inbox")]
        [HttpGet]
        public async Task<IActionResult> Inbox()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Mailler";
            ViewBag.v3 = "Mail Listesi";
            ViewBag.v0 = "Mail İşlemleri";

            var values = await _mailService.GetInboxMailAsync();
            return View(values);
        }

        [Route("GetMailDetail/{id}")]
        [HttpPost]
        public async Task<IActionResult> GetMailDetail(string id)
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Mailler";
            ViewBag.v3 = "Mail Listesi";
            ViewBag.v0 = "Mail İşlemleri";

            var values = await _mailService.GetMailDetail(id);
            return Json(values);
        }

        [Route("SendMail")]
        [HttpGet]
        public IActionResult SendMail()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Mailler";
            ViewBag.v3 = "Mail Listesi";
            ViewBag.v0 = "Mail İşlemleri";
            return View();
        }

        [Route("SendMail")]
        [HttpPost]
        public async Task<IActionResult> SendMail(MailRequest mailRequest)
        {
            try
            {
                MimeMessage mimeMessage = new MimeMessage();
                var fromAddress = _configuration["MailSettings:Mail"];

                MailboxAddress mailboxAddressFrom = new MailboxAddress("MultiShop Admin", fromAddress);
                mimeMessage.From.Add(mailboxAddressFrom);

                MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = mailRequest.MessageContent;
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = mailRequest.Subject;

                using (var smtpClient = new SmtpClient())
                {
                    var host = _configuration["MailSettings:Host"];
                    var port = int.Parse(_configuration["MailSettings:Port"]);
                    var password = _configuration["MailSettings:Password"];

                    await smtpClient.ConnectAsync(host, port, false);
                    await smtpClient.AuthenticateAsync(fromAddress, password);
                    await smtpClient.SendAsync(mimeMessage);
                    await smtpClient.DisconnectAsync(true);
                }

                var createMailDto = new CreateSentMailDto
                {
                    ReceiverMail = mailRequest.ReceiverMail,
                    Subject = mailRequest.Subject,
                    MessageContent = mailRequest.MessageContent
                };

                TempData["MailStatus"] = "Success";
                return RedirectToAction("MailList");
            }
            catch (Exception ex)
            {
                TempData["MailStatus"] = "Error";
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
