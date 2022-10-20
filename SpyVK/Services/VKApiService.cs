using SpyVK.Models.VK.Api;
using SpyVK.Services.Interfaces;
using System.Net;
using System.Text.Json;

namespace SpyVK.Services
{
    public class VKApiService : IVKApiService
    {
        public User GetSelfAccount(string token)
        {
            WebRequest request = WebRequest.Create($"https://api.vk.com/method/users.get?access_token={token}&v=5.131");
            WebResponse response = request.GetResponseAsync().GetAwaiter().GetResult();
            var result = new StreamReader(response.GetResponseStream()).ReadToEnd();
            User user = JsonSerializer.Deserialize<User>(result);
            return user;
        }
    }
}
