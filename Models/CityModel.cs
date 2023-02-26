namespace bgbrokersapi.Models
{
    public class CityModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<HoodModel> Hoods { get; set; } = new List<HoodModel>();
    }
}
