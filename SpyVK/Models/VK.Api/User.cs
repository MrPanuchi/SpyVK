namespace SpyVK.Models.VK.Api
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class User
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
    }
}
