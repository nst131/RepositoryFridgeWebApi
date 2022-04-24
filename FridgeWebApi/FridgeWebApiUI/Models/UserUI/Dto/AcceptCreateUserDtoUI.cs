using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FridgeWebApiUI.Models.UserUI.Dto
{
    public class AcceptCreateUserDtoUI
    {
        [JsonProperty(PropertyName = "userName", Order = 0, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "userEmail", Order = 1, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        [RegularExpression(@"\A[a-z0-9]+([-._][a-z0-9]+)*@([a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,4}\z", ErrorMessage = "Некоректно введен Email")]
        public string UserEmail { get; set; }
    }
}
