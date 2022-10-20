namespace SpyVK.Services.Interfaces
{
    using SpyVK.Models.VK.Api;
    public interface IVKApiService
    {
        public User GetSelfAccount(string token);
    }
}
