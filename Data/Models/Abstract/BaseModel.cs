using bgbrokersapi.Data.Models.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace bgbrokersapi.Data.Models.Abstract
{
    public class BaseModel
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public string? CreateUserId { get; set; }
        [ForeignKey(nameof(CreateUserId))]
        public ApplicationUser? CreateUser { get; set; }

        public string? UpdateUserId { get; set; }
        [ForeignKey(nameof(UpdateUserId))]
        public ApplicationUser? UpdateUser { get; set; }

        public DateTime CreateTimestamp { get; set; }
        public DateTime UpdateTimestamp { get; set; }
    }
}
