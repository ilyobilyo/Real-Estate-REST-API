using bgbrokersapi.Data.Models;

namespace bgbrokersapi.Data.Models.Types
{
    public class Furniture : Abstract.Type 
    {
        public IEnumerable<Offer> Offers { get; set; } = new List<Offer>();

    }
}
