namespace MultiShop.Mail.Dtos
{
    public class CreateSentMailDto
    {
        public string ReceiverMail { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public DateTime DateSent { get; set; }
    }
}
