namespace bgbrokersapi.Models
{
    public class AllOffersResponseModel : ResponseModel
    {
        public IEnumerable<OfferModel> Offers { get; set; }
    }
}
