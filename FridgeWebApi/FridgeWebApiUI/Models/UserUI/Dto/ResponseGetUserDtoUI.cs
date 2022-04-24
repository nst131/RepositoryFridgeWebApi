using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.UserUI.Dto
{
    public class ResponseGetUserDtoUI
    {
        [JsonProperty(PropertyName = "id", Order = 0, Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "userName", Order = 1, Required = Required.Always)]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "userEmail", Order = 2, Required = Required.Always)]
        public string UserEmail { get; set; }
    }
}
