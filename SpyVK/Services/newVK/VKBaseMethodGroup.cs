using SpyVK.Models.VK.Api;
using System.Net;
using System.Text.Json;

namespace SpyVK.Services.newVK
{
    public abstract class VKBaseMethodGroup<T>
    {
        protected string vkWay;
        protected string version;
        protected string token;
        protected string methodGroup;
        protected string method;
        protected Dictionary<string, TimeTask> tasks;

        protected VKBaseMethodGroup(string vkWay, string version, string token, string methodGroup)
        {
            this.vkWay = vkWay;
            this.version = version;
            this.token = token;
            this.methodGroup = methodGroup;
            tasks = new Dictionary<string, TimeTask>();
        }
        public Task<T> ReturnTask()
        {
            if (!tasks.ContainsKey(ConstructRequestString()))
            {
                tasks.Add(ConstructRequestString(), new TimeTask<T>(CreateNewTask()));
            }
            return tasks[ConstructRequestString()].Task as Task<T>;
        }
        private Task<T> CreateNewTask()
        {
            return new Task<T>(() => Result());
        }
        private T Result()
        {
            WebRequest request = WebRequest.Create(ConstructRequestString());
            WebResponse response = request.GetResponseAsync().GetAwaiter().GetResult();
            var data = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var result = JsonSerializer.Deserialize<T>(data);
            return result;
        }
        private string ConstructRequestString()
        {
            return vkWay + methodGroup + "." + method + "?" + AddUniqPatameters() + "&access_token=" + token + "&v=" + version;
        }
        protected abstract string AddUniqPatameters();
    }
}
