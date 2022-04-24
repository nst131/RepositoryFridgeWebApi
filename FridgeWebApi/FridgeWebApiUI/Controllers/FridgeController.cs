using AutoMapper;
using FridgeWebApiBL.Models.FridgeBL.Crud;
using FridgeWebApiBL.Models.FridgeBL.Dto;
using FridgeWebApiUI.Models.FridgeUI.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FridgeWebApiBL.Models.FridgeBL.Interfaces;
using FridgeWebApiUI.Common;
using Microsoft.AspNetCore.Authorization;

namespace FridgeWebApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeController : ControllerBase
    {
        private readonly IFridgeCrud crud;
        private readonly IMapper mapper;
        public FridgeController(IMapper mapper, IFridgeCrud crud)
        {
            this.mapper = mapper;
            this.crud = crud;
        }

        //----------------------------------------------------------------Crud Fridge------------------------------------------------------------------
        [HttpGet]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<ICollection<ResponseGetFridgeDtoUI>>> GetAll(CancellationToken token)
        {
            var fridgesBL = await this.crud.GetAllFridges(token);
            var result = fridgesBL.Select(fridge => this.mapper.Map<ResponseGetFridgeDtoUI>(fridge)).ToList();
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("[action]/{Id:int}")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<ResponseGetFridgeDtoUI>> Get([FromRoute] AcceptGetFridgeDtoUI getFridge, CancellationToken token)
        {
            var fridgeBL = await this.crud.Get(this.mapper.Map<AcceptGetFridgeDtoBL>(getFridge), token);
            return new JsonResult(this.mapper.Map<ResponseGetFridgeDtoUI>(fridgeBL));
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.Admin)]
        public async Task<ActionResult<string>> Create([FromBody] AcceptCreateFridgeDtoUI createFridge, CancellationToken token)
        {
            await this.crud.Create(this.mapper.Map<AcceptCreateFridgeDtoBL>(createFridge), token);
            return new JsonResult("Success");
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.Admin)]
        public async Task<ActionResult<string>> Update([FromBody] AcceptUpdateFridgeDtoUI updateFridge, CancellationToken token)
        {
            await this.crud.Update(this.mapper.Map<AcceptUpdateFridgeDtoBL>(updateFridge), token);
            return new JsonResult("Success");
        }

        [HttpDelete]
        [Route("[action]/{Id:int}")]
        [Authorize(Policy = PolicyService.Admin)]
        public async Task<ActionResult<string>> Delete([FromRoute] AcceptDeleteFridgeDtoUI deleteFridge, CancellationToken token)
        {
            await this.crud.Delete(this.mapper.Map<AcceptDeleteFridgeDtoBL>(deleteFridge), token);
            return new JsonResult("Success");
        }
    }
}
