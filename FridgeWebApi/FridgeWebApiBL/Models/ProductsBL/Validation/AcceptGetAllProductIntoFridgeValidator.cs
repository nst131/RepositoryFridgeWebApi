using FridgeWebApiBL.Exceptions;
using FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto;
using FridgeWebApiBL.Models.ProductsBL.Interfaces;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;
using System.Threading.Tasks;
using FridgeWebApiBL.Common.Interfaces;

namespace FridgeWebApiBL.Models.ProductsBL.Validation
{
    public class AcceptGetAllProductIntoFridgeValidator : IValidator<AcceptGetAllProductIntoFridgeDtoBL>
    {
        private readonly IDbContext context;
        private readonly IProductQueries queries;

        public AcceptGetAllProductIntoFridgeValidator(IDbContext context, IProductQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }

        public async Task<object> Validate(AcceptGetAllProductIntoFridgeDtoBL dto)
        {
            if (dto.FridgeId < 0)
                throw new ElementOutOfRangeException($"Id {nameof(FridgeProducts)} is less 0");

            var fridges = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetFridgeId(dto.FridgeId, this.context.GetDatabase));
            if (fridges.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(Fridge)} is not exist");

            return null;
        }
    }
}
