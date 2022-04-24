using AutoMapper;
using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Models.FridgeModelBL.Dto;
using FridgeWebApiBL.Models.FridgeModelBL.Interfaces;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FridgeWebApiBL.Models.FridgeModelBL.Crud
{
    public class FridgeModelCrud : IFridgeModelCrud
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;
        private readonly IValidator<AcceptGetFridgeModelDtoBL> getFridgeModelValidator;

        public FridgeModelCrud(
            IDbContext context,
            IMapper mapper,
            IValidator<AcceptGetFridgeModelDtoBL> getFridgeModelValidator)
        {
            this.context = context;
            this.mapper = mapper;
            this.getFridgeModelValidator = getFridgeModelValidator;
        }

        public async Task<ICollection<ResponseGetFridgeModelDtoBL>> GetAllFridges(CancellationToken token = default)
        {
            var fridges = await this.context.DbSet<FridgeModel>().GetAll(token);
            return fridges.Select(fridge => this.mapper.Map<ResponseGetFridgeModelDtoBL>(fridge)).ToList();
        }

        public async Task<ResponseGetFridgeModelDtoBL> Get(AcceptGetFridgeModelDtoBL getFridgeModelDto, CancellationToken token = default)
        {
            await this.getFridgeModelValidator.Validate(getFridgeModelDto);
            return this.mapper.Map<ResponseGetFridgeModelDtoBL>(await this.context.DbSet<FridgeModel>().Get(getFridgeModelDto.Id, token));
        }
    }
}
