using AutoMapper;
using FridgeWebApiBL.Models.ProductsBL.Dto.CrudDto;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Models.ProductsBL.Interfaces;

namespace FridgeWebApiBL.Models.ProductsBL.Crud
{
    public class ProductsCrud : IProductsCrud
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;
        private readonly IValidator<AcceptGetProductDtoBL> getProductValidator;
        private readonly IValidator<AcceptCreateProductDtoBL> createProductValidator;
        private readonly IValidator<AcceptUpdateProductDtoBL> updateProductValidator;
        private readonly IValidator<AcceptDeleteProductDtoBL> deleteProductValidator;

        public ProductsCrud(
            IDbContext context,
            IMapper mapper,
            IValidator<AcceptGetProductDtoBL> getProductValidator,
            IValidator<AcceptCreateProductDtoBL> createProductValidator,
            IValidator<AcceptUpdateProductDtoBL> updateProductValidator,
            IValidator<AcceptDeleteProductDtoBL> deleteProductValidator)
        {
            this.context = context;
            this.mapper = mapper;
            this.getProductValidator = getProductValidator;
            this.createProductValidator = createProductValidator;
            this.updateProductValidator = updateProductValidator;
            this.deleteProductValidator = deleteProductValidator;
        }

        public async Task<ICollection<ResponseProductDtoBL>> GetAll(CancellationToken token = default)
        {
            var products = await this.context.DbSet<Products>().GetAll(token);
            return products.Select(product => this.mapper.Map<ResponseProductDtoBL>(product)).ToList();
        }

        public async Task<ResponseProductDtoBL> Get(AcceptGetProductDtoBL getProductDto, CancellationToken token = default)
        {
            await this.getProductValidator.Validate(getProductDto);
            return this.mapper.Map<ResponseProductDtoBL>(await this.context.DbSet<Products>().Get(getProductDto.ProductId, token));
        }

        public async Task Create(AcceptCreateProductDtoBL createProduct, CancellationToken token = default)
        {
            await this.createProductValidator.Validate(createProduct);
            await this.context.DbSet<Products>().Create(this.mapper.Map<Products>(createProduct), token);
        }

        public async Task Update(AcceptUpdateProductDtoBL updateProduct, CancellationToken token = default)
        {
            await this.updateProductValidator.Validate(updateProduct);
            await this.context.DbSet<Products>().Update(this.mapper.Map<Products>(updateProduct), token);
        }

        public async Task Delete(AcceptDeleteProductDtoBL deleteProductDto, CancellationToken token = default)
        {
            await this.deleteProductValidator.Validate(deleteProductDto);
            await this.context.DbSet<Products>().Delete(deleteProductDto.ProductId, token);
        }
    }
}
