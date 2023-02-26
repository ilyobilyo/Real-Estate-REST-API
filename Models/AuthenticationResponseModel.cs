namespace bgbrokersapi.Models
{
    public class AuthenticationResponseModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

        public DateTime ValidTo { get; set; }

    }
}
