using System.ComponentModel.DataAnnotations;

namespace ExTrackAPI.Dto
{
    public class UserForRegisterDto
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}