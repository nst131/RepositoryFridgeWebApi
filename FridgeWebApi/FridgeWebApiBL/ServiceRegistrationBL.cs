using FridgeWebApiBL.Common.Interfaces;
using FridgeWebApiBL.CustomAttribute;
using FridgeWebApiBL.CustomAttribute.Interfaces;
using FridgeWebApiBL.Models.FridgeBL.Crud;
using FridgeWebApiBL.Models.FridgeBL.Dto;
using FridgeWebApiBL.Models.FridgeBL.Fetchers;
using FridgeWebApiBL.Models.FridgeBL.Interfaces;
using FridgeWebApiBL.Models.FridgeBL.Validation;
using FridgeWebApiBL.Models.FridgeModelBL.Crud;
using FridgeWebApiBL.Models.FridgeModelBL.Dto;
using FridgeWebApiBL.Models.FridgeModelBL.Fetchers;
using FridgeWebApiBL.Models.FridgeModelBL.Interfaces;
using FridgeWebApiBL.Models.FridgeModelBL.Validation;
using FridgeWebApiBL.Models.ProductsBL.Crud;
using FridgeWebApiBL.Models.ProductsBL.Dto.CrudDto;
using FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto;
using FridgeWebApiBL.Models.ProductsBL.Fetchers;
using FridgeWebApiBL.Models.ProductsBL.Interfaces;
using FridgeWebApiBL.Models.ProductsBL.Validation;
using FridgeWebApiBL.Models.UserBL.Crud;
using FridgeWebApiBL.Models.UserBL.Dto;
using FridgeWebApiBL.Models.UserBL.Fetchers;
using FridgeWebApiBL.Models.UserBL.Interfaces;
using FridgeWebApiBL.Models.UserBL.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeWebApiBL
{
    public static class ServiceRegistrationBL
    {
        public static void AddRegistrationBL(this IServiceCollection service)
        {
            //Attributes
            service.AddScoped<IUniqueName, UniqueName>();

            //Products
            service.AddScoped<IProductProcedures, ProductProcedures>();
            service.AddScoped<IProductQueries, ProductQueries>();
            service.AddScoped<IProductsFetchersBL, ProductsFetchersBL>();
            service.AddScoped<IValidator<AcceptAddProductIntoFridgeDtoBL>, AcceptAddProductsIntoFridgeValidator>();
            service.AddScoped<IValidator<AcceptUpdateProductIntoFridgeDtoBL>, AcceptUpdateProductsIntoFridgeValidator>();
            service.AddScoped<IValidator<AcceptDeleteProductIntoFridgeDtoBL>, AcceptDeleteProductIntoFridgeValidator>();
            service.AddScoped<IValidator<AcceptGetAllProductIntoFridgeDtoBL>, AcceptGetAllProductIntoFridgeValidator>();
            service.AddScoped<IValidator<AcceptUpdateProductIntoFridgeByIdDtoBL>, AcceptUpdateProductIntoFridgeByIdValidator>();
            service.AddScoped<IValidator<AcceptDeleteProductIntoFridgeByIdDtoBL>, AcceptDeleteProductsIntoFridgeByIdValidator>();
            service.AddScoped<IValidator<AcceptGetProductIntoFridgeByIdDtoBL>, AcceptGetProductIntoFridgeByIdValidator>();

            service.AddScoped<IProductsCrud, ProductsCrud>();
            service.AddScoped<IValidator<AcceptGetProductDtoBL>, AcceptGetProductValidator>();
            service.AddScoped<IValidator<AcceptCreateProductDtoBL>, AcceptCreateProductValidator>();
            service.AddScoped<IValidator<AcceptUpdateProductDtoBL>, AcceptUpdateProductValidator>();
            service.AddScoped<IValidator<AcceptDeleteProductDtoBL>, AcceptDeleteProductValidator>();

            //Fridge
            service.AddScoped<IFridgeQueries, FridgeQueries>();
            service.AddScoped<IFridgeCrud, FridgeCrud>();
            service.AddScoped<IValidator<AcceptGetFridgeDtoBL>, AcceptGetFridgeValidator>();
            service.AddScoped<IValidator<AcceptCreateFridgeDtoBL>, AcceptCreateFridgeValidator>();
            service.AddScoped<IValidator<AcceptUpdateFridgeDtoBL>, AcceptUpdateFridgeValidator>();
            service.AddScoped<IValidator<AcceptDeleteFridgeDtoBL>, AcceptDeleteFridgeValidator>();

            //User
            service.AddScoped<IUserCrud, UserCrud>();
            service.AddScoped<IUserQueries, UserQueries>();
            service.AddScoped<IValidator<AcceptCreateUserDtoBL>, AcceptCreateUserValidator>();
            service.AddScoped<IValidator<AcceptGetUserDtoBL>, AcceptGetUserValidator>();

            //FridgeModel
            service.AddScoped<IFridgeModelCrud, FridgeModelCrud>();
            service.AddScoped<IFridgeModelQueries, FridgeModelQueries>();
            service.AddScoped<IValidator<AcceptGetFridgeModelDtoBL>, AcceptGetFridgeModelValidator>();
        }
    }
}
