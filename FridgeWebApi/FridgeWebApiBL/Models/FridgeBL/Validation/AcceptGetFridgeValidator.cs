using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiBL.Models.FridgeBL.Dto;
using FridgeWebApiBL.Models.FridgeBL.Interfaces;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;
using System.Threading.Tasks;

namespace FridgeWebApiBL.Models.FridgeBL.Validation
{
    public class AcceptGetFridgeValidator : IValidator<AcceptGetFridgeDtoBL>
    {
        private readonly IDbContext context;
        private readonly IFridgeQueries queries;

        public AcceptGetFridgeValidator(IDbContext context, IFridgeQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }
        public async Task<object> Validate(AcceptGetFridgeDtoBL dto)
        {
            if (dto.FridgeId < 0)
                throw new ElementOutOfRangeException($"{nameof(AcceptGetFridgeDtoBL.FridgeId)} in {nameof(AcceptGetFridgeDtoBL)} cann't less 0");

            var element = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetFridgeId(dto.FridgeId, this.context.GetDatabase));
            if (element.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(Fridge)} is not exist");

            return null;
        }
    }
}
