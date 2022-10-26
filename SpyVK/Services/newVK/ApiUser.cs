using SpyVK.Models.VK.Api;

namespace SpyVK.Services.newVK
{
    public class ApiUser : VKBaseMethodGroup<Response<User>>
    {
        public ApiUser(string vkWay, string version, string token) : base(vkWay, version, token, "user")
        {

        }
        protected override string AddUniqPatameters()
        {
            throw new NotImplementedException();
        }
    }
}
