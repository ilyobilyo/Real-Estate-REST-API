using AutoMapper;
using bgbrokersapi.Data.Models.User;
using bgbrokersapi.Models;

namespace bgbrokersapi.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserModel>();
                
        }
    }
}
