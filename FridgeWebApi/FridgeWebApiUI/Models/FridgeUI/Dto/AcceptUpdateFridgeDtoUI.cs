using System.ComponentModel.DataAnnotations;
using FridgeWebApiUI.CustomAttribute;
using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.FridgeUI.Dto
{
    [AttributeUniqueFridgeNameForUpdate(nameof(AcceptUpdateFridgeDtoUI.Id),nameof(AcceptUpdateFridgeDtoUI.Name))]
    public class AcceptUpdateFridgeDtoUI
    {
        [JsonProperty(PropertyName = "id", Order = 0, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name", Order = 1, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]

        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "fridgeModelId", Order = 2, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int FridgeModelId { get; set; }

        [JsonProperty(PropertyName = "userId", Order = 3, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int UserId { get; set; }
    }
}
