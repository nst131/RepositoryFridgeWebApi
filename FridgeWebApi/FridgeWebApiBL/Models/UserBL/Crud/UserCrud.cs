using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Models.UserBL.Dto;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using System.Threading;
using System.Threading.Tasks;
using FridgeWebApiBL.Models.FridgeModelBL.Dto;
using FridgeWebApiBL.Models.UserBL.Interfaces;

namespace FridgeWebApiBL.Models.UserBL.Crud
{
    public class UserCrud : IUserCrud
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;
        private readonly IValidator<AcceptCreateUserDtoBL> createUserValidator;
        private readonly IValidator<AcceptGetUserDtoBL> getUserValidator;
        public UserCrud(
            IDbContext context,
            IMapper mapper,
            IValidator<AcceptCreateUserDtoBL> createUserValidator,
            IValidator<AcceptGetUserDtoBL> getUserValidator)
        {
            this.context = context;
            this.mapper = mapper;
            this.createUserValidator = createUserValidator;
            this.getUserValidator = getUserValidator;
        }

        public async Task<ICollection<ResponseGetUserDtoBL>> GetAllUsers(CancellationToken token = default)
        {
            var users = await this.context.DbSet<User>().GetAll(token);
            return users.Select(user => this.mapper.Map<ResponseGetUserDtoBL>(user)).ToList();
        }

        public async Task<ResponseGetUserDtoBL> Get(AcceptGetUserDtoBL getUserDto, CancellationToken token = default)
        {
            await this.getUserValidator.Validate(getUserDto);
            return this.mapper.Map<ResponseGetUserDtoBL>(await this.context.DbSet<User>().Get(getUserDto.Id, token));
        }
        public async Task Create(AcceptCreateUserDtoBL createUser, CancellationToken token = default)
        {
            await this.createUserValidator.Validate(createUser);
            await this.context.DbSet<User>().Create(this.mapper.Map<User>(createUser), token);
        }
    }
}
