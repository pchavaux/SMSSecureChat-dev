namespace SMSChat.Client.Services
{
    public class SmsRequest
    {
        public string Did { get; set; }
        public string Dst { get; set; }
        public string Message { get; set; }
    }
}
