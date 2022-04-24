using System.Threading.Tasks;
using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiBL.Models.FridgeBL.Dto;
using FridgeWebApiBL.Models.FridgeBL.Interfaces;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;

namespace FridgeWebApiBL.Models.FridgeBL.Validation
{
    public class AcceptDeleteFridgeValidator : IValidator<AcceptDeleteFridgeDtoBL>
    {
        private readonly IDbContext context;
        private readonly IFridgeQueries queries;

        public AcceptDeleteFridgeValidator(IDbContext context, IFridgeQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }
        public async Task<object> Validate(AcceptDeleteFridgeDtoBL dto)
        {
            if (dto.FridgeId < 0)
                throw new ElementOutOfRangeException($"{nameof(Fridge.Id)} in {nameof(Fridge)} cann't less 0");

            var fridge = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetFridgeId(dto.FridgeId, this.context.GetDatabase));
            if (fridge.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(Fridge)} is not exist");

            return null;
        }
    }
}
