using bgbrokersapi.Data.Models.User;

namespace bgbrokersapi.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Town { get; set; }

        //public string? Role { get; set; }
    }
}
