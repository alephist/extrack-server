using System.ComponentModel.DataAnnotations;

namespace ExTrackAPI.Dto
{
    public class UserForLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}