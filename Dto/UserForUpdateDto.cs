using System.ComponentModel.DataAnnotations;

namespace ExTrackAPI.Dto
{
    public class UserForUpdateDto
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}