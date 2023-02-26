using System.ComponentModel.DataAnnotations;

namespace bgbrokersapi.Models.InputModels
{
    public class LoginInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
