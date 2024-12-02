using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        public byte[] Password { get; set; }

        public byte[] PasswordKey { get; set; }
    }
}