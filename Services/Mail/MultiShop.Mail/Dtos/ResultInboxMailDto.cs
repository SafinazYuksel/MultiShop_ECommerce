namespace MultiShop.Mail.Dtos
{
    public class ResultInboxMailDto
    {
        public string MessageId { get; set; }
        public string SenderName { get; set; }
        public string SenderMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }
}
