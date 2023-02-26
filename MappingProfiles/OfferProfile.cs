using AutoMapper;
using bgbrokersapi.Data.Models;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;

namespace bgbrokersapi.MappingProfiles
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<Offer, OfferModel>()
                .ForMember(x => x.Broker, y => y.MapFrom(s => s.Broker));
            CreateMap<Offer, OfferUpdateModel>();
        }
    }
}
