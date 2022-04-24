using System.Linq;
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
    public class AcceptUpdateProductsIntoFridgeValidator : IValidator<AcceptUpdateProductIntoFridgeDtoBL>
    {
        private readonly IDbContext context;
        private readonly IProductQueries queries;

        public AcceptUpdateProductsIntoFridgeValidator(IDbContext context, IProductQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }
        public async Task<object> Validate(AcceptUpdateProductIntoFridgeDtoBL dto)
        {
            var fridgeProducts = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetFridgeProductsId(dto.FridgeId, dto.ProductId, this.context.GetDatabase));
            if (fridgeProducts.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(FridgeProducts)} is not exist");

            var products = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetProductId(dto.ProductId, this.context.GetDatabase));
            if (products.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(Products)} is not exist");

            var fridges = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetFridgeId(dto.FridgeId, this.context.GetDatabase));
            if (fridges.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(Fridge)} is not exist");

            if (dto.Quantity < 0)
                throw new ElementOutOfRangeException($"{nameof(dto.Quantity)} cann't less 0");

            return fridgeProducts.FirstOrDefault()!.Id;
        }
    }
}
