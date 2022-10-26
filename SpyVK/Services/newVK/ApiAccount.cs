using SpyVK.Models.VK.Api;

namespace SpyVK.Services.newVK
{
    public class ApiAccount : VKBaseMethodGroup<Response<User>>
    {
        public ApiAccount(string vkWay, string version, string token) : base(vkWay, version, token, "account")
        {

        }

        protected override string AddUniqPatameters()
        {
            throw new NotImplementedException();
        }
    }
}