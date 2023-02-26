using bgbrokersapi.Models.InputModels;

namespace bgbrokersapi.Models
{
    public class UpdateOfferResponseModel : ResponseModel
    {
        public OfferUpdateModel Offer { get; set; }
    }
}
