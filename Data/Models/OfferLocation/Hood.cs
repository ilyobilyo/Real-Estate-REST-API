using bgbrokersapi.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bgbrokersapi.Data.Models.OfferLocation
{
    public class Hood : Abstract.Type 
    {
        public int? CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        public IEnumerable<Offer> Offers { get; set; } = new List<Offer>();
    }
}
