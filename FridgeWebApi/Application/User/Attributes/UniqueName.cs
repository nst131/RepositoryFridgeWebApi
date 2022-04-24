using EFData;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.User.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class UniqueName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (DataContext)validationContext.GetService(typeof(DataContext));

            if (service is null)
                throw new NullReferenceException($"{nameof(service)} is null check your connection");

            return service.Users.AnyAsync(x => x.UserName == (string)value).Result ? new ValidationResult("Name has existed yet") : null;
        }
    }
}
