using Newtonsoft.Json;

namespace Application.User
{
    public class User
    {
        [JsonProperty(PropertyName = "Email", Order = 0, Required = Required.Always)]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "Email", Order = 1, Required = Required.Always)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Email", Order = 2, Required = Required.Always)]
        public string Role { get; set; }
    }
}
