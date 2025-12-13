namespace MultiShop.Mail.Dtos
{
    public class GetByIdSentMailDto
    {
        public int SentMailId { get; set; }
        public string ReceiverMail { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public DateTime DateSent { get; set; }
    }
}
