using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.ProductUI.Dto
{
    public class ResponseGetProductIntoFridgeDtoUI
    {
        [JsonProperty(PropertyName = "fridgeProductId", Order = 0, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int FridgeProductId { get; set; }

        [JsonProperty(PropertyName = "fridgeId", Order = 1, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int FridgeId { get; set; }

        [JsonProperty(PropertyName = "productId", Order = 2, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "quantity", Order = 3, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int Quantity { get; set; }
    }
}
