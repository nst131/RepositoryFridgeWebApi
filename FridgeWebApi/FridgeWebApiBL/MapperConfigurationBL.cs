using AutoMapper;
using FridgeWebApiBL.Models.FridgeBL.Dto;
using FridgeWebApiBL.Models.FridgeModelBL.Dto;
using FridgeWebApiBL.Models.ProductsBL.Dto.CrudDto;
using FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto;
using FridgeWebApiBL.Models.UserBL.Dto;
using FridgeWebApiDL.Entity;

namespace FridgeWebApiBL
{
    public class MapperConfigurationBL : Profile
    {
        public MapperConfigurationBL()
        {
            //---------------------FridgeProducts-------------------------
            CreateMap<AcceptUpdateProductIntoFridgeDtoBL, FridgeProducts>();
            CreateMap<AcceptAddProductIntoFridgeDtoBL, FridgeProducts>();
            CreateMap<FridgeProducts, ResponseGetProductIntoFridgeByIdDtoBL>()
                .ForMember(x => x.FridgeProductId, y => y.MapFrom(z => z.Id));

            CreateMap<AcceptUpdateProductIntoFridgeByIdDtoBL, FridgeProducts>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.FridgeProductId));
            //------------------------Fridge------------------------------
            CreateMap<Fridge, ResponseFridgeDtoBL>()
                .ForMember(x => x.ModelName, y => y.MapFrom(z => z.FridgeModel.Name))
                .ForMember(x => x.OwnerName, y => y.MapFrom(z => z.User.UserName));
            CreateMap<AcceptCreateFridgeDtoBL, Fridge>();
            CreateMap<AcceptUpdateFridgeDtoBL, Fridge>();

            //-----------------------Products-----------------------------
            CreateMap<Products, ResponseProductDtoBL>();
            CreateMap<AcceptCreateProductDtoBL, Products>();
            CreateMap<AcceptUpdateProductDtoBL, Products>();

            //------------------------User--------------------------------
            CreateMap<AcceptCreateUserDtoBL, User>();
            CreateMap<User, ResponseGetUserDtoBL>();

            //----------------------FridgeModel---------------------------
            CreateMap<FridgeModel, ResponseGetFridgeModelDtoBL>();
        }
    }
}
