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
    public class AcceptGetProductValidator : IValidator<AcceptGetProductDtoBL>
    {
        private readonly IDbContext context;
        private readonly IProductQueries queries;

        public AcceptGetProductValidator(IDbContext context, IProductQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }

        public async Task<object> Validate(AcceptGetProductDtoBL dto)
        {
            if (dto.ProductId < 0)
                throw new ElementOutOfRangeException($"Id {nameof(Products)} is less 0");

            var element = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetProductId(dto.ProductId, this.context.GetDatabase));
            if (element.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(Products)} is not exist");

            return null;
        }
    }
}
