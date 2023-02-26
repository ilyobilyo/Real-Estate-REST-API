namespace bgbrokersapi.Models
{
    public class RegisterResponseModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public UserModel User { get; set; }

    }
}
