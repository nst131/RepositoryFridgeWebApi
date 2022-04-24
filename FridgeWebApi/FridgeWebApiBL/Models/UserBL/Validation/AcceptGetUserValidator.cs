using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiBL.Models.UserBL.Dto;
using FridgeWebApiBL.Models.UserBL.Interfaces;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;
using System.Threading.Tasks;

namespace FridgeWebApiBL.Models.UserBL.Validation
{
    public class AcceptGetUserValidator : IValidator<AcceptGetUserDtoBL>
    {
        private readonly IDbContext context;
        private readonly IUserQueries queries;
        public AcceptGetUserValidator(IDbContext context, IUserQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }
        public async Task<object> Validate(AcceptGetUserDtoBL dto)
        {
            if (dto.Id < 0)
                throw new ElementOutOfRangeException($"{nameof(dto.Id)} in {nameof(AcceptGetUserDtoBL)} cann't less 0");

            var element = await this.context.ExecuteQueryAndRead<Entity>(this.queries.QueryGetUserId(dto.Id, this.context.GetDatabase));
            if (element.Count == 0)
                throw new ElementByIdNotFoundException($"{nameof(User)} is not exist");

            return null;
        }
    }
}
