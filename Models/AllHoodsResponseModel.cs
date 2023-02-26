namespace bgbrokersapi.Models
{
    public class AllHoodsResponseModel : ResponseModel
    {
        public IEnumerable<HoodModel> Hoods { get; set; }
    }
}
