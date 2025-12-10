using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Payment.Concrete;
using MultiShop.Payment.Entities;

namespace MultiShop.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentContext _context;

        public PaymentsController(PaymentContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(PaymentDetail paymentDetail)
        {
            await _context.PaymentDetails.AddAsync(paymentDetail);
            await _context.SaveChangesAsync();

            return Ok("Ödeme kaydı başarıyla oluşturuldu.");
        }
    }
}
