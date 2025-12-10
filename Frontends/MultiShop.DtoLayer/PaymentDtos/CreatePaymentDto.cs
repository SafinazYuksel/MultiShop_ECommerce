using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.PaymentDtos
{
    public class CreatePaymentDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string OrderId { get; set; }
        public decimal TotalPrice { get; set; }

        // Kart Bilgileri (Simülasyon için)
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string CVV { get; set; }
    }
}
