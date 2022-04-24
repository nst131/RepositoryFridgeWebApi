using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.ProductUI.Dto
{
    public class ResponseSearchProductsIntoFridgeDtoUI
    {
        [JsonProperty(PropertyName = "id", Order = 0, Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "fridgeId", Order = 1, Required = Required.Always)]
        public int FridgeId { get; set; }

        [JsonProperty(PropertyName = "productId", Order = 2, Required = Required.Always)]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "productName", Order = 3, Required = Required.Always)]
        public string ProductName { get; set; }
        
        [JsonProperty(PropertyName = "defaultQuantity", Order = 4, Required = Required.Always)]
        public int DefaultQuantity { get; set; }

        [JsonProperty(PropertyName = "fridgeName", Order = 5, Required = Required.Always)]
        public string FridgeName { get; set; }
    }
}
