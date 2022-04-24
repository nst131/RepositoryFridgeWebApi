using AutoMapper;
using FridgeWebApiBL.Models.FridgeModelBL.Dto;
using FridgeWebApiBL.Models.FridgeModelBL.Interfaces;
using FridgeWebApiUI.Common;
using FridgeWebApiUI.Models.FridgeModelUI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FridgeWebApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeModelController : ControllerBase
    {
        public readonly IFridgeModelCrud crud;
        public readonly IMapper mapper;

        public FridgeModelController(IFridgeModelCrud crud, IMapper mapper)
        {
            this.crud = crud;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<ICollection<ResponseGetFridgeModelDtoUI>>> GetAll(CancellationToken token)
        {
            var fridgeModelsBL = await this.crud.GetAllFridges(token);
            var result = fridgeModelsBL.Select(fridgeModel => this.mapper.Map<ResponseGetFridgeModelDtoUI>(fridgeModel)).ToList();
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("[action]/{Id:int}")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<ResponseGetFridgeModelDtoUI>> Get([FromRoute] AcceptGetFridgeModelDtoUI getFridgeModel, CancellationToken token)
        {
            var fridgeModleBL = await this.crud.Get(this.mapper.Map<AcceptGetFridgeModelDtoBL>(getFridgeModel), token);
            return new JsonResult(this.mapper.Map<ResponseGetFridgeModelDtoUI>(fridgeModleBL));
        }
    }
}
