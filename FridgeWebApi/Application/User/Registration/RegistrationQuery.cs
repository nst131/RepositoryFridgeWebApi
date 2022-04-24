using System.ComponentModel.DataAnnotations;
using Application.User.Attributes;
using Newtonsoft.Json;

namespace Application.User.Registration
{
    public class RegistrationQuery
    {
        [JsonProperty(PropertyName = "Email", Order = 0, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки Email должна быть от 3 до 20 символов")]
        [RegularExpression(@"\A[a-z0-9]+([-._][a-z0-9]+)*@([a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,4}\z", ErrorMessage = "Некоректно введен Email")]
        [UniqueEmail]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Name", Order = 1, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки Name должна быть от 3 до 20 символов")]
        [UniqueName]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Password", Order = 2, Required = Required.Always)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки Password должна быть от 3 до 10 символов")]
        public string Password { get; set; }
    }
}
