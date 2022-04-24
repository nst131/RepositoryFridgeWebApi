using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.ProductUI.Dto
{
    public class AcceptGetProductIntoFridgeDtoUI
    {
        [JsonProperty(PropertyName = "id", Order = 0, Required = Required.Always)]
        [Range(0, int.MaxValue, ErrorMessage = "Значение вышло за пределы допустимого диапозона")]
        public int Id { get; set; }
    }
}
