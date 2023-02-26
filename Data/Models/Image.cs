using bgbrokersapi.Data.Models;

namespace bgbrokersapi.Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Guid? OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
