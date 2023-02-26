using bgbrokersapi.Data.Models;

namespace bgbrokersapi.Data.Models.OfferLocation
{
    public class City : Abstract.Type 
    {
        public IEnumerable<Hood> Hoods { get; set; } = new List<Hood>();

        public IEnumerable<Offer> Offers { get; set; } = new List<Offer>();
    }
}
