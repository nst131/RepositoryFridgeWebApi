using System;
using System.ComponentModel.DataAnnotations;
using FridgeWebApiBL.CustomAttribute.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiDL.Entity;

namespace FridgeWebApiUI.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class AttributeUniqueProductName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IUniqueName)validationContext.GetService(typeof(IUniqueName));

            if (service is null)
                throw new ElementNullReferenceException($"{nameof(service)} is null check your connection");

            return service.IsUnique<Products>((string)value).Result ? null : new ValidationResult($"{nameof(Products.Name)} has existed yet");
        }
    }
}
