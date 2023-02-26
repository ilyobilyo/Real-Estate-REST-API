using AutoMapper;
using bgbrokersapi.Data.Models.Types;
using bgbrokersapi.Models;

namespace bgbrokersapi.MappingProfiles
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {
            CreateMap<Construction, TypeModel>();
            CreateMap<Currency, TypeModel>();
            CreateMap<Exposition, TypeModel>();
            CreateMap<Furniture, TypeModel>();
            CreateMap<Heating, TypeModel>();
            CreateMap<SellType, TypeModel>();
            CreateMap<OfferType, TypeModel>();
            CreateMap<Construction, TypeOfferModel>();
            CreateMap<Currency, TypeOfferModel>();
            CreateMap<Exposition, TypeOfferModel>();
            CreateMap<Furniture, TypeOfferModel>();
            CreateMap<Heating, TypeOfferModel>();
            CreateMap<SellType, TypeOfferModel>();
            CreateMap<OfferType, TypeOfferModel>();
        }
    }
}
