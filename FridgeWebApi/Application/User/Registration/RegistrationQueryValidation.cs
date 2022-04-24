using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Application.User.Registration
{
    public class RegistrationQueryValidation
    {
        public virtual async Task<Validation> ValidateAsync(RegistrationQuery query)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(query.Email))
                errors.Add(new ValidationResult($"{nameof(RegistrationQuery.Email)} is not correct"));

            if (string.IsNullOrEmpty(query.Name))
                errors.Add(new ValidationResult($"{nameof(RegistrationQuery.Name)} is not correct"));

            if (string.IsNullOrEmpty(query.Password))
                errors.Add(new ValidationResult($"{nameof(RegistrationQuery.Password)} is not correct"));

            await Task.CompletedTask;

            return new Validation { Errors = errors };
        }
    }
}
