using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Application.User.Login
{
    public class LoginQuery
    {
        [JsonProperty(PropertyName = "Email", Order = 0, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина Email строки должна быть от 3 до 20 символов")]
        [RegularExpression(@"\A[a-z0-9]+([-._][a-z0-9]+)*@([a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,4}\z", ErrorMessage = "Некоректно введен Email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Password", Order = 1, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина Password строки должна быть от 3 до 10 символов")]
        public string Password { get; set; }
    }
}
