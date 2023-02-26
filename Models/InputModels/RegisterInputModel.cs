using System.ComponentModel.DataAnnotations;

namespace bgbrokersapi.Models.InputModels
{
    public class RegisterInputModel
    {
        //[Required]
        //[MaxLength(100)]
        public string Username { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

       //[DataType(DataType.Password)]
       //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //[Required]
        //[MaxLength(100)]
        public string FirstName { get; set; }

        //[Required]
        //[MaxLength(100)]
        public string LastName { get; set; }

        //[Required]
        //[EmailAddress]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Town { get; set; }
    }
}
