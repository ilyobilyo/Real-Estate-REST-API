using bgbrokersapi.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace bgbrokersapi.Data.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Town { get; set; }

        public IEnumerable<Offer> CreatedOffers { get; set; } = new List<Offer>();

        public IEnumerable<Offer> UpdatedOffers { get; set; } = new List<Offer>();
    }
}
