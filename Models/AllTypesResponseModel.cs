namespace bgbrokersapi.Models
{
    public class AllTypesResponseModel : ResponseModel
    {
        public IEnumerable<TypeModel> Types { get; set; }
    }
}
