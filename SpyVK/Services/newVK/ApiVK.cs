namespace SpyVK.Services.newVK
{
    public class ApiVK
    {
        private readonly string vkWay = "https://api.vk.com/method/";
        private readonly string version = "5.131";
        private Dictionary<string, ApiAccount> _accounts;
        private Dictionary<string, ApiFriend> _friends;
        private Dictionary<string, ApiUser> _users;

        public ApiVK()
        {
            _accounts = new Dictionary<string, ApiAccount>();
            _friends = new Dictionary<string, ApiFriend>();
            _users = new Dictionary<string, ApiUser>();
        }
        public ApiAccount FromAccount(string sessionId, string token)
        {
            if (!_accounts.ContainsKey(sessionId))
            {
                _accounts.Add(sessionId, new ApiAccount(vkWay, version, token));
            }
            return _accounts[sessionId];
        }
        public ApiFriend FromFriend(string sessionId, string token)
        {
            if (!_friends.ContainsKey(sessionId))
            {
                _friends.Add(sessionId, new ApiFriend(vkWay, version, token));
            }
            return _friends[sessionId];
        }
        public ApiUser FromUser(string sessionId, string token)
        {
            if (!_users.ContainsKey(sessionId))
            {
                _users.Add(sessionId,new ApiUser(vkWay, version, token));
            }
            return _users[sessionId];
        }
    }
}
