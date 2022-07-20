using System.ComponentModel.DataAnnotations;

namespace MartEdu.Service.DTOs.Users
{
    public class UserForLoginDto
    {
        [Required, MinLength(4), MaxLength(32)]
        public string EmailOrUsername { get; set; }

        [Required, MinLength(8), MaxLength(64)]
        public string Password { get; set; }
    }
}
