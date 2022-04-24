using Application.User.Registration;
using AuthApi.Models;
using AutoMapper;

namespace AuthApi
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<RegistrationQuery, RegistrationEntity>()
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.UserEmail, y => y.MapFrom(z => z.Email));
        }
    }
}
