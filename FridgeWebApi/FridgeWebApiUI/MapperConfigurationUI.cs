using AutoMapper;
using FridgeWebApiBL.Models.FridgeBL.Dto;
using FridgeWebApiBL.Models.FridgeModelBL.Dto;
using FridgeWebApiBL.Models.ProductsBL.Dto.CrudDto;
using FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto;
using FridgeWebApiBL.Models.UserBL.Dto;
using FridgeWebApiUI.Models.FridgeModelUI.Dto;
using FridgeWebApiUI.Models.FridgeUI.Dto;
using FridgeWebApiUI.Models.ProductUI.Dto;
using FridgeWebApiUI.Models.UserUI.Dto;

namespace FridgeWebApiUI
{
    public class MapperConfigurationUI : Profile
    {
        public MapperConfigurationUI()
        {
            //------------------------------------Fridge---------------------------------------
            //Crud
            CreateMap<AcceptGetFridgeDtoUI, AcceptGetFridgeDtoBL>()
                .ForMember(x => x.FridgeId, y => y.MapFrom(z => z.Id));
            CreateMap<AcceptCreateFridgeDtoUI, AcceptCreateFridgeDtoBL>();
            CreateMap<AcceptUpdateFridgeDtoUI, AcceptUpdateFridgeDtoBL>();
            CreateMap<AcceptDeleteFridgeDtoUI, AcceptDeleteFridgeDtoBL>()
                .ForMember(x => x.FridgeId, y => y.MapFrom(z => z.Id));
            CreateMap<ResponseFridgeDtoBL, ResponseGetFridgeDtoUI>();

            //-----------------------------------Products---------------------------------------
            //Fetchers
            CreateMap<AcceptGetAllProductsIntoFriedgeDtoUI, AcceptGetAllProductIntoFridgeDtoBL>()
                .ForMember(x => x.FridgeId, y => y.MapFrom(z => z.Id));
            CreateMap<AcceptGetProductIntoFridgeDtoUI, AcceptGetProductIntoFridgeByIdDtoBL>()
                .ForMember(x => x.FridgeProductId, y => y.MapFrom(z => z.Id));
            CreateMap<AcceptAddProductsIntoFridgeDtoUI, AcceptAddProductIntoFridgeDtoBL>();
            CreateMap<AcceptUpdateProductsIntoFridgeDtoUI, AcceptUpdateProductIntoFridgeDtoBL>();
            CreateMap<AcceptDeleteProductsIntoFridgeDtoUI, AcceptDeleteProductIntoFridgeDtoBL>();
            CreateMap<ResponseProductIntoFridgeDtoBL, ResponseProductIntoFridgeDtoUI>()
                .ForMember(x => x.FridgeProductId, y => y.MapFrom(z => z.FridgeProducts.Id))
                .ForMember(x => x.ProductId, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Quantity, y => y.MapFrom(z => z.FridgeProducts.Quantity));

            CreateMap<AcceptUpdateProductIntoFridgeByIdDtoUI, AcceptUpdateProductIntoFridgeByIdDtoBL>();
            CreateMap<AcceptDeleteProductsIntoFridgeByIdDtoUI, AcceptDeleteProductIntoFridgeByIdDtoBL>()
                .ForMember(x => x.FridgeProductId, y => y.MapFrom(z => z.Id));
            CreateMap<ResponseGetProductIntoFridgeByIdDtoBL, ResponseGetProductIntoFridgeDtoUI>();

            //Procedure
            CreateMap<ResponseSearchProductsIntoFridgeDtoBL, ResponseSearchProductsIntoFridgeDtoUI>();

            //Crud
            CreateMap<AcceptGetProductDtoUI, AcceptGetProductDtoBL>()
                .ForMember(x => x.ProductId, y => y.MapFrom(z => z.Id));
            CreateMap<AcceptCreateProductDtoUI, AcceptCreateProductDtoBL>();
            CreateMap<AcceptUpdateProductDtoUI, AcceptUpdateProductDtoBL>();
            CreateMap<AcceptDeleteProductDtoUI, AcceptDeleteProductDtoBL>()
                .ForMember(x => x.ProductId, y => y.MapFrom(y => y.Id));
            CreateMap<ResponseProductDtoBL, ResponseProductsDtoUI>();

            //------------------------------------User----------------------------------------------
            //Crud
            CreateMap<AcceptCreateUserDtoUI, AcceptCreateUserDtoBL>();
            CreateMap<AcceptGetUserDtoUI, AcceptGetUserDtoBL>();
            CreateMap<ResponseGetUserDtoBL, ResponseGetUserDtoUI>();

            //----------------------------------FridgeModel-----------------------------------------
            //Crud
            CreateMap<AcceptGetFridgeModelDtoUI, AcceptGetFridgeModelDtoBL>();
            CreateMap<ResponseGetFridgeModelDtoBL, ResponseGetFridgeModelDtoUI>();
        }
    }
}
