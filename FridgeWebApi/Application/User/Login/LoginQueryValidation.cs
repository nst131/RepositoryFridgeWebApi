using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Application.User.Login
{
    public class LoginQueryValidation
    {
        public virtual async Task<Validation> ValidateAsync(LoginQuery query)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(query.Email))
                errors.Add(new ValidationResult($"{nameof(LoginQuery.Email)} is not correct"));

            if (string.IsNullOrEmpty(query.Password))
                errors.Add(new ValidationResult($"{nameof(LoginQuery.Password)} is not correct"));

            await Task.CompletedTask;

            return new Validation { Errors = errors };
        }
    }
}
