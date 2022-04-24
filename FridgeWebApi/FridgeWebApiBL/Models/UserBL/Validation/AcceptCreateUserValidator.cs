using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Exceptions;
using FridgeWebApiBL.Models.UserBL.Dto;
using FridgeWebApiBL.Models.UserBL.Interfaces;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Helper;
using System.Threading.Tasks;

namespace FridgeWebApiBL.Models.UserBL.Validation
{
    public class AcceptCreateUserValidator : IValidator<AcceptCreateUserDtoBL>
    {
        private readonly IDbContext context;
        private readonly IUserQueries queries;
        public AcceptCreateUserValidator(IDbContext context, IUserQueries queries)
        {
            this.context = context;
            this.queries = queries;
        }

        public async Task<object> Validate(AcceptCreateUserDtoBL dto)
        {
            if (string.IsNullOrEmpty(dto.UserName))
                throw new ElementNullReferenceException($"{nameof(dto.UserName)} cann't be null or empty");

            if (string.IsNullOrEmpty(dto.UserEmail))
                throw new ElementNullReferenceException($"{nameof(dto.UserEmail)} cann't be null or empty");

            var elementEmail = await context.ExecuteQueryAndRead<Entity>(this.queries.QueryCheckUniqueEmail(dto.UserEmail, this.context.GetDatabase));
            if (elementEmail.Count != 0)
                throw new ElementByIdNotFoundException($"{nameof(dto.UserEmail)} is exist yet");

            var elementName = await context.ExecuteQueryAndRead<Entity>(this.queries.QueryCheckUniqueName(dto.UserName, this.context.GetDatabase));
            if (elementName.Count != 0)
                throw new ElementByIdNotFoundException($"{nameof(dto.UserName)} is exist yet");

            return null;
        }
    }
}
