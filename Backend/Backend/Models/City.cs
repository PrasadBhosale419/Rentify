using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class City
    {
        public int id { get; set; }

        public string? name { get; set; }

        [Required]
        public string Country { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public int LastUpdatedBy { get; set; }
    }
}
