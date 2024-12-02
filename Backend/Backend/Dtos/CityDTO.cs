using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos
{
    public class CityDTO
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Name field is mandatory")]
        [StringLength(50, MinimumLength =2)]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Only Numerics are not allowed")]
        public string? name { get; set; }

        [Required(ErrorMessage ="Country field is mandatory")]
        public string Country { get; set; }
    }
}
