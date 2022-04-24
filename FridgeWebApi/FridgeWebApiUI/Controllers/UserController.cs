using AutoMapper;
using FridgeWebApiBL.Models.UserBL.Dto;
using FridgeWebApiBL.Models.UserBL.Interfaces;
using FridgeWebApiUI.Common;
using FridgeWebApiUI.Models.UserUI.Dto;
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
    public class UserController : ControllerBase
    {
        public readonly IUserCrud crud;
        public readonly IMapper mapper;

        public UserController(IUserCrud crud, IMapper mapper)
        {
            this.crud = crud;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<ICollection<ResponseGetUserDtoUI>>> GetAll(CancellationToken token)
        {
            var usersBL = await this.crud.GetAllUsers(token);
            var result = usersBL.Select(user => this.mapper.Map<ResponseGetUserDtoUI>(user)).ToList();
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("[action]/{Id:int}")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<ResponseGetUserDtoUI>> Get([FromRoute] AcceptGetUserDtoUI getUser, CancellationToken token)
        {
            var userBL = await this.crud.Get(this.mapper.Map<AcceptGetUserDtoBL>(getUser), token);
            return new JsonResult(this.mapper.Map<ResponseGetUserDtoUI>(userBL));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<string>> Create([FromBody] AcceptCreateUserDtoUI createFridge, CancellationToken token)
        {
            await this.crud.Create(this.mapper.Map<AcceptCreateUserDtoBL>(createFridge), token);
            return new JsonResult("Success");
        }
    }
}
