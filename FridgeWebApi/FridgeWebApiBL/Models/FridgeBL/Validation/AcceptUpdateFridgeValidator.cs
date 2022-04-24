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
    public class AcceptUpdateFridgeValidator : IValidator<AcceptUpdateFridgeDtoBL>
    {
        private readonly IDbContext context;
        private readonly IFridgeQueries queries;

        public AcceptUpdateFridgeValidator(IDbContext context, IFridgeQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }
        public async Task<object> Validate(AcceptUpdateFridgeDtoBL dto)
        {
            var fridge = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetFridgeId(dto.Id, this.context.GetDatabase));
            if (fridge.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(Fridge)} is not exist");

            var fridgeModel = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetFridgeModelId(dto.FridgeModelId, this.context.GetDatabase));
            if (fridgeModel.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(FridgeModel)} is not exist");

            var user = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetUserId(dto.UserId, this.context.GetDatabase));
            if (user.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(User)} is not exist");

            return null;
        }
    }
}
