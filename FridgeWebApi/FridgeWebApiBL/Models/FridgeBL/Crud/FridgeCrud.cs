using AutoMapper;
using FridgeWebApiBL.Models.FridgeBL.Dto;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Models.FridgeBL.Interfaces;

namespace FridgeWebApiBL.Models.FridgeBL.Crud
{
    public class FridgeCrud : IFridgeCrud
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;
        private readonly IValidator<AcceptGetFridgeDtoBL> getFridgeValidator;
        private readonly IValidator<AcceptCreateFridgeDtoBL> createFridgeValidator;
        private readonly IValidator<AcceptUpdateFridgeDtoBL> updateFridgeValidator;
        private readonly IValidator<AcceptDeleteFridgeDtoBL> deleteFridgeValidator;

        public FridgeCrud(
            IMapper mapper,
            IDbContext context,
            IValidator<AcceptGetFridgeDtoBL> getFridgeValidator,
            IValidator<AcceptCreateFridgeDtoBL> createFridgeValidator,
            IValidator<AcceptUpdateFridgeDtoBL> updateFridgeValidator,
            IValidator<AcceptDeleteFridgeDtoBL> deleteFridgeValidator)
        {
            this.mapper = mapper;
            this.context = context;
            this.getFridgeValidator = getFridgeValidator;
            this.createFridgeValidator = createFridgeValidator;
            this.updateFridgeValidator = updateFridgeValidator;
            this.deleteFridgeValidator = deleteFridgeValidator;
        }

        public async Task<ICollection<ResponseFridgeDtoBL>> GetAllFridges(CancellationToken token = default)
        {
            var fridges = await this.context.DbSet<Fridge>().GetAll(token);
            return fridges.Select(fridge => this.mapper.Map<ResponseFridgeDtoBL>(fridge)).ToList();
        }

        public async Task<ResponseFridgeDtoBL> Get(AcceptGetFridgeDtoBL getFridgeDto, CancellationToken token = default)
        {
            await this.getFridgeValidator.Validate(getFridgeDto);
            return this.mapper.Map<ResponseFridgeDtoBL>(await this.context.DbSet<Fridge>().Get(getFridgeDto.FridgeId, token));
        }

        public async Task Create(AcceptCreateFridgeDtoBL createFridge, CancellationToken token = default)
        {
            await this.createFridgeValidator.Validate(createFridge);
            await this.context.DbSet<Fridge>().Create(this.mapper.Map<Fridge>(createFridge), token);
        }

        public async Task Update(AcceptUpdateFridgeDtoBL updateFridge, CancellationToken token = default)
        {
            await this.updateFridgeValidator.Validate(updateFridge);
             await this.context.DbSet<Fridge>().Update(this.mapper.Map<Fridge>(updateFridge), token);
        }

        public async Task Delete(AcceptDeleteFridgeDtoBL deleteFridge, CancellationToken token = default)
        {
            await this.deleteFridgeValidator.Validate(deleteFridge);
            await this.context.DbSet<Fridge>().Delete(deleteFridge.FridgeId, token);
        }
    }
}
