namespace bgbrokersapi.Models
{
    public class AllCitiesResponseModel : ResponseModel
    {
        public IEnumerable<CityModel> Cities { get; set; }
    }
}
