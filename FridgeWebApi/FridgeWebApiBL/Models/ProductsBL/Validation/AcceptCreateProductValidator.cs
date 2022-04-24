using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiBL.Models.ProductsBL.Dto.CrudDto;
using FridgeWebApiDL.Entity;
using System.Threading.Tasks;

namespace FridgeWebApiBL.Models.ProductsBL.Validation
{
    public class AcceptCreateProductValidator : IValidator<AcceptCreateProductDtoBL>
    {
        public async Task<object> Validate(AcceptCreateProductDtoBL dto)
        {
            if (dto.DefaultQuantity < 0)
                throw new ElementOutOfRangeException($"{nameof(Products.DefaultQuantity)} {nameof(Products)} is less 0");

            await Task.CompletedTask;
            return null;
        }
    }
}
