using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.ProductUI.Dto
{
    public class ResponseProductIntoFridgeDtoUI
    {
        [JsonProperty(PropertyName = "fridgeProductId", Order = 0, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int FridgeProductId { get; set; }

        [JsonProperty(PropertyName = "productId", Order = 1, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "name", Order = 2, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "quantity", Order = 3, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int Quantity { get; set; }
    }
}
