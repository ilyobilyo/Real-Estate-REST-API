using AutoMapper;
using bgbrokersapi.Data.Models;
using bgbrokersapi.Models;

namespace bgbrokersapi.MappingProfiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageModel>();
        }
    }
}
