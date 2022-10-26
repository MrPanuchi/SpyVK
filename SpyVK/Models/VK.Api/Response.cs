using System.Text.Json.Serialization;

namespace SpyVK.Models.VK.Api
{
    public class Response<T>
    {
        [JsonPropertyName("count")]
        public int count { get; set; }

        [JsonPropertyName("response")]
        public IEnumerable<T> Values { get; set; }
    }
}
