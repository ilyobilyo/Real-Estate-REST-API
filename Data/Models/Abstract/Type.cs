using System.ComponentModel.DataAnnotations;

namespace bgbrokersapi.Data.Models.Abstract
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
