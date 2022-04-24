using System.Linq;
using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto;
using FridgeWebApiBL.Models.ProductsBL.Interfaces;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;
using System.Threading.Tasks;

namespace FridgeWebApiBL.Models.ProductsBL.Validation
{
    public class AcceptDeleteProductIntoFridgeValidator : IValidator<AcceptDeleteProductIntoFridgeDtoBL>
    {
        private readonly IDbContext context;
        private readonly IProductQueries queries;

        public AcceptDeleteProductIntoFridgeValidator(IDbContext context, IProductQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }
        public async Task<object> Validate(AcceptDeleteProductIntoFridgeDtoBL dto)
        {
            if (dto.FridgeId < 0)
                throw new ElementOutOfRangeException($"{nameof(FridgeProducts.FridgeId)} in {nameof(FridgeProducts)} is less 0");

            if (dto.ProductId < 0)
                throw new ElementOutOfRangeException($"{nameof(FridgeProducts.ProductId)} in {nameof(FridgeProducts)} is less 0");

            var fridges = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetFridgeProductsId(dto.FridgeId, dto.ProductId, this.context.GetDatabase));
            if (fridges.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(FridgeProducts)} is not exist");

            return fridges.FirstOrDefault()!.Id;
        }
    }
}
