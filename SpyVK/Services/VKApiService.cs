using SpyVK.Models.VK.Api;
using SpyVK.Services.Interfaces;
using System.Net;
using System.Text.Json;

namespace SpyVK.Services
{
    public class VKApiService : IVKApiService
    {
        private readonly string version = "5.131";
        private readonly string vkWay = "https://api.vk.com/method/";
        private readonly Dictionary<string, Type> methodToType = new Dictionary<string, Type>
        {
            {"users.get", typeof(Response<User>) },
            {"friends.get", typeof (Response<User>) }
        };
        private readonly Dictionary<string>
        public User GetSelfAccount(string token)
        {
            WebRequest request = WebRequest.Create($"https://api.vk.com/method/users.get?access_token={token}&v=5.131");
            WebResponse response = request.GetResponseAsync().GetAwaiter().GetResult();
            var result = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var user = JsonSerializer.Deserialize<Response<User>>(result).Values.First();
            return user;
        }
    }
}
