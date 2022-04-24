using System.Threading.Tasks;
using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiBL.Models.ProductsBL.Dto.CrudDto;
using FridgeWebApiBL.Models.ProductsBL.Interfaces;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;

namespace FridgeWebApiBL.Models.ProductsBL.Validation
{
    public class AcceptUpdateProductValidator : IValidator<AcceptUpdateProductDtoBL>
    {
        private readonly IDbContext context;
        private readonly IProductQueries queries;

        public AcceptUpdateProductValidator(IDbContext context, IProductQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }
        public async Task<object> Validate(AcceptUpdateProductDtoBL dto)
        {
            var element = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetProductId(dto.Id, this.context.GetDatabase));
            if (element.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(Products)} is not exist");

            if (dto.DefaultQuantity < 0)
                throw new ElementOutOfRangeException($"{nameof(Products.DefaultQuantity)} {nameof(Products)} is less 0");

            return null;
        }
    }
}
