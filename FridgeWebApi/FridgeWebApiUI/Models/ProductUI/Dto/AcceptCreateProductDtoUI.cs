using System.ComponentModel.DataAnnotations;
using FridgeWebApiUI.CustomAttribute;
using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.ProductUI.Dto
{
    public class AcceptCreateProductDtoUI
    {
        [JsonProperty(PropertyName = "name", Order = 0, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        [AttributeUniqueProductName]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "defaultQuantity", Order = 1, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int DefaultQuantity { get; set; }
    }
}
