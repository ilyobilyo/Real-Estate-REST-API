using bgbrokersapi.Data.Models;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using System.Security.Claims;

namespace bgbrokersapi.Services.Offers
{
    public interface IOfferService
    {
        Task<IEnumerable<Offer>> GetAllOffers();
        Task<Offer> GetById(Guid id);
        Task<OfferResponseModel> CreateOffer(OfferInputModel model, Claim createUserIdClaim);
        Task<UpdateOfferResponseModel> UpdateOffer(Guid id, OfferInputModel model, Claim updateUserIdClaim);
        Task<ResponseModel> DelteFromDb(Guid id);
        Task<ResponseModel> CasualDelete(Guid id);
    }
}
