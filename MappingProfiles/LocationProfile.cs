using AutoMapper;
using bgbrokersapi.Data.Models.OfferLocation;
using bgbrokersapi.Models;

namespace bgbrokersapi.MappingProfiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<City, CityModel>();
            CreateMap<Hood, HoodModel>()
                .ForMember(x => x.City, y => y.MapFrom(s => s.City.Name));

        }
    }
}
