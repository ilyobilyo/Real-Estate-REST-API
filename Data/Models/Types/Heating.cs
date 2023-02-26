using bgbrokersapi.Data.Models;

namespace bgbrokersapi.Data.Models.Types
{
    public class Heating : Abstract.Type
    {
        public IEnumerable<Offer> Offers { get; set; } = new List<Offer>();

    }
}
