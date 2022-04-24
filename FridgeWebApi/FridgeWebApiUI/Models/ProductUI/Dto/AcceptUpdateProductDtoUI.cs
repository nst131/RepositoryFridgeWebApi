﻿using FridgeWebApiUI.CustomAttribute;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FridgeWebApiUI.Models.ProductUI.Dto
{
    [AttributeUniqueProductNameForUpdate(nameof(AcceptUpdateProductDtoUI.Id), nameof(AcceptUpdateProductDtoUI.Name))]
    public class AcceptUpdateProductDtoUI
    {
        [JsonProperty(PropertyName = "id", Order = 0, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name", Order = 1, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "defaultQuantity", Order = 2, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int DefaultQuantity { get; set; }
    }
}
