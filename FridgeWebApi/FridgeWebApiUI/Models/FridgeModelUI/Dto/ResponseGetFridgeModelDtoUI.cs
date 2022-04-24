using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.FridgeModelUI.Dto
{
    public class ResponseGetFridgeModelDtoUI
    {
        [JsonProperty(PropertyName = "id", Order = 0, Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name", Order = 1, Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "year", Order = 2, Required = Required.Always)]
        public int? Year { get; set; }
    }
}
