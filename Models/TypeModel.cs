namespace bgbrokersapi.Models
{
    public class TypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<OfferModel> Offers { get; set; }
    }
}
