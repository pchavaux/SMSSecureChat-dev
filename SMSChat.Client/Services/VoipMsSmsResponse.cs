using SMSChat.Client.Services;

namespace SMSChat.Client.Services
{
    public class VoipMsSmsResponse
    {
        public IEnumerable<SmsMessage> Sms { get; set; }
    }
}
