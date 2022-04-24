using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.ProductUI.Dto
{
    public class AcceptUpdateProductIntoFridgeByIdDtoUI
    {
        [JsonProperty(PropertyName = "fridgeProductId", Order = 0, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int FridgeProductId { get; set; }

        [JsonProperty(PropertyName = "quantity", Order = 1, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int Quantity { get; set; }
    }
}
