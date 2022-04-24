using System.Threading.Tasks;
using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto;
using FridgeWebApiBL.Models.ProductsBL.Interfaces;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;

namespace FridgeWebApiBL.Models.ProductsBL.Validation
{
    public class AcceptUpdateProductIntoFridgeByIdValidator : IValidator<AcceptUpdateProductIntoFridgeByIdDtoBL>
    {
        private readonly IDbContext context;
        private readonly IProductQueries queries;

        public AcceptUpdateProductIntoFridgeByIdValidator(IDbContext context, IProductQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }
        public async Task<object> Validate(AcceptUpdateProductIntoFridgeByIdDtoBL dto)
        {
            if (dto.FridgeProductId < 0)
                throw new ElementOutOfRangeException($"{dto.FridgeProductId} cannot be zero");

            var entity = await this.context.ExecuteQueryAndRead<Entity>(
                this.queries.QueryOnExistFridgeProduct(dto.FridgeProductId, this.context.GetDatabase));

            if(entity.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(FridgeProducts)} is not exist");

            return null;
        }
    }
}
