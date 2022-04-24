using AutoMapper;
using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto;
using FridgeWebApiBL.Models.ProductsBL.Interfaces;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FridgeWebApiBL.Models.ProductsBL.Fetchers
{
    public class ProductsFetchersBL : IProductsFetchersBL
    {
        private readonly IDbContext context;
        private readonly IMapper mapper;
        private readonly IProductProcedures procedures;
        private readonly IProductQueries queries;
        private readonly IValidator<AcceptAddProductIntoFridgeDtoBL> addProductIntoFridgeValidator;
        private readonly IValidator<AcceptUpdateProductIntoFridgeDtoBL> updateProductIntoFridgeValidator;
        private readonly IValidator<AcceptGetAllProductIntoFridgeDtoBL> getProductIntoFridgeValidator;
        private readonly IValidator<AcceptDeleteProductIntoFridgeDtoBL> deleteProductIntoFridgeValidator;
        private readonly IValidator<AcceptUpdateProductIntoFridgeByIdDtoBL> updateProductIntoFridgeByIdValidator;
        private readonly IValidator<AcceptDeleteProductIntoFridgeByIdDtoBL> deleteProductIntoFridgeByIdValidator;
        private readonly IValidator<AcceptGetProductIntoFridgeByIdDtoBL> getProductIntoFridgeByIdValidator;

        public ProductsFetchersBL(
            IDbContext context,
            IMapper mapper,
            IProductProcedures procedures,
            IProductQueries queries,
            IValidator<AcceptAddProductIntoFridgeDtoBL> addProductIntoFridgeValidator,
            IValidator<AcceptUpdateProductIntoFridgeDtoBL> updateProductIntoFridgeValidator,
            IValidator<AcceptGetAllProductIntoFridgeDtoBL> getProductIntoFridgeValidator,
            IValidator<AcceptDeleteProductIntoFridgeDtoBL> deleteProductIntoFridgeValidator,
            IValidator<AcceptUpdateProductIntoFridgeByIdDtoBL> updateProductIntoFridgeByIdValidator,
            IValidator<AcceptDeleteProductIntoFridgeByIdDtoBL> deleteProductIntoFridgeByIdValidator,
            IValidator<AcceptGetProductIntoFridgeByIdDtoBL> getProductIntoFridgeByIdValidator)
        {
            this.context = context;
            this.mapper = mapper;
            this.procedures = procedures;
            this.queries = queries;
            this.addProductIntoFridgeValidator = addProductIntoFridgeValidator;
            this.updateProductIntoFridgeValidator = updateProductIntoFridgeValidator;
            this.getProductIntoFridgeValidator = getProductIntoFridgeValidator;
            this.deleteProductIntoFridgeValidator = deleteProductIntoFridgeValidator;
            this.updateProductIntoFridgeByIdValidator = updateProductIntoFridgeByIdValidator;
            this.deleteProductIntoFridgeByIdValidator = deleteProductIntoFridgeByIdValidator;
            this.getProductIntoFridgeByIdValidator = getProductIntoFridgeByIdValidator;
        }

        public async Task<ICollection<ResponseProductIntoFridgeDtoBL>> GetAllProductsByFridgeId(AcceptGetAllProductIntoFridgeDtoBL getProductByFridgeId,
            CancellationToken token = default)
        {
            await this.getProductIntoFridgeValidator.Validate(getProductByFridgeId);
            var products = await this.context.ExecuteQueryAndRead<ResponseProductIntoFridgeDtoBL, FridgeProducts>(
                this.queries.QueryGetProductsByFridgeId(context.GetDatabase, getProductByFridgeId.FridgeId), token);
            return products;
        }

        public async Task<ResponseGetProductIntoFridgeByIdDtoBL> GetProductIntoFridgeById(AcceptGetProductIntoFridgeByIdDtoBL getProductIntoFridge,
            CancellationToken token)
        {
            await this.getProductIntoFridgeByIdValidator.Validate(getProductIntoFridge);
            var fridgeProduct = await this.context.DbSet<FridgeProducts>().Get(getProductIntoFridge.FridgeProductId, token);
            return this.mapper.Map<ResponseGetProductIntoFridgeByIdDtoBL>(fridgeProduct);
        }

        public async Task AddProductIntoFridge(AcceptAddProductIntoFridgeDtoBL addProductsIntoFridge,
            CancellationToken token = default)
        {
            var fridgeProductObject = await this.addProductIntoFridgeValidator.Validate(addProductsIntoFridge);
            switch (fridgeProductObject)
            {
                case null:
                    {
                        var fridgeProduct = this.mapper.Map<FridgeProducts>(addProductsIntoFridge);
                        await this.context.DbSet<FridgeProducts>().Create(fridgeProduct, token);
                        break;
                    }
                case FridgeProducts fridgeProduct:
                    {
                        fridgeProduct.Quantity += addProductsIntoFridge.Quantity;
                        await this.context.DbSet<FridgeProducts>().Update(fridgeProduct, token);
                        break;
                    }
            }
        }

        public async Task UpdateProductIntoFridge(AcceptUpdateProductIntoFridgeDtoBL updateProductsIntoFridge,
            CancellationToken token = default)
        {
            var fridgeProductId = await this.updateProductIntoFridgeValidator.Validate(updateProductsIntoFridge);
            var productsFridge = this.mapper.Map<FridgeProducts>(updateProductsIntoFridge);
            productsFridge.Id = (int)fridgeProductId;
            await this.context.DbSet<FridgeProducts>().Update(productsFridge, token);
        }

        public async Task UpdateProductIntoFridgeById(AcceptUpdateProductIntoFridgeByIdDtoBL updateProductIntoFridge,
            CancellationToken token)
        {
            await this.updateProductIntoFridgeByIdValidator.Validate(updateProductIntoFridge);
            await this.context.DbSet<FridgeProducts>().Update(this.mapper.Map<FridgeProducts>(updateProductIntoFridge), token);
        }

        public async Task DeleteProductsIntoFridge(AcceptDeleteProductIntoFridgeDtoBL deleteProductIntoFridge,
            CancellationToken token = default)
        {
            var fridgeProductId = await this.deleteProductIntoFridgeValidator.Validate(deleteProductIntoFridge);
            await this.context.DbSet<FridgeProducts>().Delete((int)fridgeProductId, token);
        }

        public async Task DeleteProductsIntoFridgeById(AcceptDeleteProductIntoFridgeByIdDtoBL deleteProductIntoFridge,
            CancellationToken token)
        {
            await this.deleteProductIntoFridgeByIdValidator.Validate(deleteProductIntoFridge);
            await this.context.DbSet<FridgeProducts>().Delete(deleteProductIntoFridge.FridgeProductId, token);
        }

        public async Task<ResponseSearchProductsIntoFridgeDtoBL> SearchProductsIntoFridgeWhereQuantityZero()
        {
            return await this.context.TryExecuteProcedure<ResponseSearchProductsIntoFridgeDtoBL>(
                NameProductsProcedures.ProcedureSearchProductsInFridge,
                this.procedures.CreateProcedureSearchProductsIntoFridge,
                this.procedures.GetModelFromProcedureSearchProductsIntoFridge);
        }


    }
}
