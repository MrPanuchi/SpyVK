namespace SpyVK.Models.VK.Api
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class User
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        [JsonPropertyName("photo_50")]
        public string PhotoUrl { get; set; }

        [JsonPropertyName("online")]
        public bool Online { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
    }
}
