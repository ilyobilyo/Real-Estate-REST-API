namespace bgbrokersapi.Models.InputModels
{
    public class AddRoleToUserInputModel
    {
        public string UserId { get; set; }

        public IList<string> RolesToAdd { get; set; } = new List<string>();
    }
}
