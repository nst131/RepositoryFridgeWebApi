using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.ProductUI.Dto
{
    public class AcceptDeleteProductsIntoFridgeDtoUI
    {
        [JsonProperty(PropertyName = "fridgeId", Order = 0, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int FridgeId { get; set; }

        [JsonProperty(PropertyName = "productId", Order = 1, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int ProductId { get; set; }
    }
}
