using AutoMapper;
using FridgeWebApiBL.Models.ProductsBL.Dto.CrudDto;
using FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto;
using FridgeWebApiBL.Models.ProductsBL.Interfaces;
using FridgeWebApiUI.Models.ProductUI.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FridgeWebApiUI.Common;
using Microsoft.AspNetCore.Authorization;

namespace FridgeWebApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsFetchersBL productsFetchersBl;
        private readonly IProductsCrud productsCrudBl;
        private readonly IMapper mapper;

        public ProductController(IProductsFetchersBL productsFetchersBl, IMapper mapper, IProductsCrud productsCrudBl)
        {
            this.productsFetchersBl = productsFetchersBl;
            this.mapper = mapper;
            this.productsCrudBl = productsCrudBl;
        }

        //------------------------------------------------------Crud FridgeProduct----------------------------------------------------------------
        [HttpGet]
        [Route("[action]/{Id:int}")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<ResponseProductIntoFridgeDtoUI>> GetAllProductsIntoFridge([FromRoute] AcceptGetAllProductsIntoFriedgeDtoUI getAllProductsIntoFriedge,
            CancellationToken token)
        {
            var allProductsIntoFridge = await this.productsFetchersBl.GetAllProductsByFridgeId(this.mapper.Map<AcceptGetAllProductIntoFridgeDtoBL>(getAllProductsIntoFriedge), token);
            var result = allProductsIntoFridge.Select(product => this.mapper.Map<ResponseProductIntoFridgeDtoUI>(product)).ToList();
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("[action]/{Id:int}")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<ResponseGetProductIntoFridgeDtoUI>> GetProductIntoFridge([FromRoute] AcceptGetProductIntoFridgeDtoUI getProduct, 
            CancellationToken token)
        {
            var productIntoFridgeBL = await this.productsFetchersBl.GetProductIntoFridgeById(this.mapper.Map<AcceptGetProductIntoFridgeByIdDtoBL>(getProduct), token);
            return new JsonResult(this.mapper.Map<ResponseGetProductIntoFridgeDtoUI>(productIntoFridgeBL));
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<string>> AddProductIntoFridge([FromBody] AcceptAddProductsIntoFridgeDtoUI addProductsIntoFridge,
            CancellationToken token)
        {
            await this.productsFetchersBl.AddProductIntoFridge(this.mapper.Map<AcceptAddProductIntoFridgeDtoBL>(addProductsIntoFridge), token);
            return new JsonResult("Success");
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<string>> UpdateProductIntoFridge([FromBody] AcceptUpdateProductsIntoFridgeDtoUI updateProductsIntoFridge,
            CancellationToken token)
        {
            await this.productsFetchersBl.UpdateProductIntoFridge(this.mapper.Map<AcceptUpdateProductIntoFridgeDtoBL>(updateProductsIntoFridge), token);
            return new JsonResult("Success");
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<string>> UpdateProductIntoFridgeById([FromBody] AcceptUpdateProductIntoFridgeByIdDtoUI updateProductsIntoFridge,
            CancellationToken token)
        {
            await this.productsFetchersBl.UpdateProductIntoFridgeById(this.mapper.Map<AcceptUpdateProductIntoFridgeByIdDtoBL>(updateProductsIntoFridge), token);
            return new JsonResult("Success");
        }

        [HttpDelete]
        [Route("[action]/{Id:int}")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<string>> DeleteProductIntoFridge([FromRoute] AcceptDeleteProductsIntoFridgeDtoUI deleteProductsIntoFridge,
            CancellationToken token)
        {
            await this.productsFetchersBl.DeleteProductsIntoFridge(this.mapper.Map<AcceptDeleteProductIntoFridgeDtoBL>(deleteProductsIntoFridge), token);
            return new JsonResult("Success");
        }

        [HttpDelete]
        [Route("[action]/{Id:int}")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<string>> DeleteProductIntoFridgeById([FromRoute] AcceptDeleteProductsIntoFridgeByIdDtoUI deleteProductsIntoFridge,
            CancellationToken token)
        {
            await this.productsFetchersBl.DeleteProductsIntoFridgeById(this.mapper.Map<AcceptDeleteProductIntoFridgeByIdDtoBL>(deleteProductsIntoFridge), token);
            return new JsonResult("Success");
        }

        //-------------------------------------------------------Procedure--------------------------------------------------------------------------
        [HttpGet]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<ResponseSearchProductsIntoFridgeDtoUI>> SearchProductsInFridgeWhereZero(CancellationToken token)
        {
            var productsWithQuantityZeroBl = await this.productsFetchersBl.SearchProductsIntoFridgeWhereQuantityZero();
            return new JsonResult(this.mapper.Map<ResponseSearchProductsIntoFridgeDtoUI>(productsWithQuantityZeroBl));
        }

        //-----------------------------------------------------Crud Product-------------------------------------------------------------------------
        [HttpGet]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<ICollection<ResponseProductsDtoUI>>> GetAll(CancellationToken token)
        {
            var productsBL = await this.productsCrudBl.GetAll(token);
            var result = productsBL.Select(product => this.mapper.Map<ResponseProductsDtoUI>(product)).ToList();
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("[action]/{Id:int}")]
        [Authorize(Policy = PolicyService.AllAccess)]
        public async Task<ActionResult<ResponseProductsDtoUI>> Get([FromRoute] AcceptGetProductDtoUI getProduct, CancellationToken token)
        {
            var productBL = await this.productsCrudBl.Get(this.mapper.Map<AcceptGetProductDtoBL>(getProduct), token);
            return new JsonResult(this.mapper.Map<ResponseProductsDtoUI>(productBL));
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.Admin)]
        public async Task<ActionResult<string>> Create([FromBody] AcceptCreateProductDtoUI createProduct, CancellationToken token)
        {
            await this.productsCrudBl.Create(this.mapper.Map<AcceptCreateProductDtoBL>(createProduct), token);
            return new JsonResult("Success");
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Policy = PolicyService.Admin)]
        public async Task<ActionResult<string>> Update([FromBody] AcceptUpdateProductDtoUI updateProduct, CancellationToken token)
        {
            await this.productsCrudBl.Update(this.mapper.Map<AcceptUpdateProductDtoBL>(updateProduct), token);
            return new JsonResult("Success");
        }

        [HttpDelete]
        [Route("[action]/{Id:int}")]
        [Authorize(Policy = PolicyService.Admin)]
        public async Task<ActionResult<string>> Delete([FromRoute] AcceptDeleteProductDtoUI deleteProduct, CancellationToken token)
        {
            await this.productsCrudBl.Delete(this.mapper.Map<AcceptDeleteProductDtoBL>(deleteProduct), token);
            return new JsonResult("Success");
        }
    }
}
