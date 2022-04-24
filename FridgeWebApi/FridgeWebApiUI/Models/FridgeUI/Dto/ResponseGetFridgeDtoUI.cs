using Newtonsoft.Json;

#nullable enable
namespace FridgeWebApiUI.Models.FridgeUI.Dto
{
    public class ResponseGetFridgeDtoUI
    {
        [JsonProperty(PropertyName = "id", Order = 0, Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name", Order = 1, Required = Required.Always)]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "ownerName", Order = 2, Required = Required.Default)]
        public string? OwnerName { get; set; }

        [JsonProperty(PropertyName = "modelName", Order = 3, Required = Required.Always)]
        public string ModelName { get; set; } = string.Empty;
    }
}
