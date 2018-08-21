using AutoMapper;
using WebApplication.Repository.Entities;

namespace WebApplication.Services
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, Models.User>();
            CreateMap<HighScore, Models.HighScore>();
        }
    }
}
