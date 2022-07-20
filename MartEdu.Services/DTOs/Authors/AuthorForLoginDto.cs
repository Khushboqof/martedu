using System.ComponentModel.DataAnnotations;

namespace MartEdu.Service.DTOs.Authors
{
    public class AuthorForLoginDto
    {
        [Required, MinLength(4), MaxLength(32)]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(64)]
        public string Password { get; set; }
    }
}
