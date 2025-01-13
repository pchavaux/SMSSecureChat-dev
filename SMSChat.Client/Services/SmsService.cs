using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using SMSChat.Models;
namespace SMSChat.Client.Services
{
    public class SmsService
    {
        private readonly HttpClient _httpClient;

        public SmsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendSmsAsync(string did, string dst, string message)
        {
            var request = new SmsRequest { Did = did, Dst = dst, Message = message };
            var response = await _httpClient.PostAsJsonAsync("api/sms/send", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<SmsMessage>> GetSmsMessagesAsync(string did)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<SmsMessage>>($"api/sms/messages?did={did}");
            return response;
        }
    }
}
