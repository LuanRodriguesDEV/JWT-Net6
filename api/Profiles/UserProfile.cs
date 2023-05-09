using api.Model;
using api.VOs.User;
using AutoMapper;

namespace api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserVOEnter, User>()
                .ForPath(dest => dest.Name, opts => opts.MapFrom(x => x.Name))
                .ForPath(dest => dest.Email, opts => opts.MapFrom(x => x.Email))
                .ForPath(dest => dest.Password, opts => opts.MapFrom(x => x.Password));
        }
    }
}
