namespace SMSChat.Client.Services
{
    using System.Net.Http.Json;
    using global::SMSChat.Models;
 

    public class FriendsService
    {
        private readonly HttpClient _httpClient;

        public FriendsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Friend>> GetFriendsAsync(string phoneNumber)
        {
            return await _httpClient.GetFromJsonAsync<List<Friend>>($"api/friends?phoneNumber={phoneNumber}");
        }

        public async Task<bool> AddFriendAsync(Friend friend)
        {
            var response = await _httpClient.PostAsJsonAsync("api/friends", friend);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateFriendAsync(Friend friend)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/friends/{friend.Id}", friend);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFriendAsync(int friendId)
        {
            var response = await _httpClient.DeleteAsync($"api/friends/{friendId}");
            return response.IsSuccessStatusCode;
        }
    }

}
