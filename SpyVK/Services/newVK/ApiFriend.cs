using SpyVK.Models.VK.Api;

namespace SpyVK.Services.newVK
{
    public class ApiFriend : VKBaseMethodGroup<Response<User>>
    {
        public ApiFriend(string vkWay, string version, string token) : base(vkWay, version, token, "friend")
        {

        }
        protected override string AddUniqPatameters()
        {
            throw new NotImplementedException();
        }
    }
}
