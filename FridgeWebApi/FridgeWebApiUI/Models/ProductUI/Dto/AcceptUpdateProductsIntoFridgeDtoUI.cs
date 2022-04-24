using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.ProductUI.Dto
{
    public class AcceptUpdateProductsIntoFridgeDtoUI
    {
        [JsonProperty(PropertyName = "productId", Order = 0, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "fridgeId", Order = 1, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int FridgeId { get; set; }

        [JsonProperty(PropertyName = "quantity", Order = 2, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int Quantity { get; set; }
    }
}
