using System;
using System.ComponentModel.DataAnnotations;
using FridgeWebApiBL.CustomAttribute.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiDL.Entity;

namespace FridgeWebApiUI.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AttributeUniqueProductNameForUpdate : ValidationAttribute
    {
        private readonly string idAbbreviation;
        private readonly string nameAbbreviation;

        public AttributeUniqueProductNameForUpdate(string idAbbreviation, string nameAbbreviation)
        {
            this.idAbbreviation = idAbbreviation;
            this.nameAbbreviation = nameAbbreviation;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IUniqueName)validationContext.GetService(typeof(IUniqueName));

            var id = value.GetType().GetProperty(this.idAbbreviation)?.GetValue(value);
            var name = value.GetType().GetProperty(this.nameAbbreviation)?.GetValue(value);

            if (id is null || name is null)
                return new ValidationResult("Don't correct use attribute");

            if (service is null)
                throw new ElementNullReferenceException($"{nameof(service)} is null check your connection");

            return service.IsUniqueForUpdate<Products>((string)name, (int)id!).Result ? null : new ValidationResult($"{nameof(Products.Name)} has existed yet");
        }
    }
}
