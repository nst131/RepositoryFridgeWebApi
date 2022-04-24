using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiBL.Models.FridgeModelBL.Dto;
using FridgeWebApiBL.Models.FridgeModelBL.Interfaces;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;
using System.Threading.Tasks;

namespace FridgeWebApiBL.Models.FridgeModelBL.Validation
{
    public class AcceptGetFridgeModelValidator : IValidator<AcceptGetFridgeModelDtoBL>
    {
        private readonly IDbContext context;
        private readonly IFridgeModelQueries queries;

        public AcceptGetFridgeModelValidator(IDbContext context, IFridgeModelQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }

        public async Task<object> Validate(AcceptGetFridgeModelDtoBL dto)
        {
            if (dto.Id < 0)
                throw new ElementOutOfRangeException($"{nameof(dto.Id)} in {nameof(AcceptGetFridgeModelDtoBL)} cann't less 0");

            var element = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetFridgeModelId(dto.Id, this.context.GetDatabase));
            if (element.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(FridgeModel)} is not exist");

            return null;
        }
    }
}
