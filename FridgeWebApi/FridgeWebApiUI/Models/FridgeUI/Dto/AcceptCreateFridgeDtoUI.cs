using System.ComponentModel.DataAnnotations;
using FridgeWebApiUI.CustomAttribute;
using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.FridgeUI.Dto
{
    public class AcceptCreateFridgeDtoUI
    {
        [JsonProperty(PropertyName = "name", Order = 0, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        [AttributeUniqueFridgeName]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "fridgeModelId", Order = 1, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int FridgeModelId { get; set; }

        [JsonProperty(PropertyName = "userId", Order = 2, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int UserId { get; set; }
    }
}
