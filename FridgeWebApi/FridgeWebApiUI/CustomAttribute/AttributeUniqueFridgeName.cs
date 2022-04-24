using FridgeWebApiBL.CustomAttribute.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiDL.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace FridgeWebApiUI.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class AttributeUniqueFridgeName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IUniqueName)validationContext.GetService(typeof(IUniqueName));

            if (service is null)
                throw new ElementNullReferenceException($"{nameof(service)} is null check your connection");

            return service.IsUnique<Fridge>((string) value).Result ? null : new ValidationResult($"{nameof(Fridge.Name)} has existed yet");
        }
    }
}
