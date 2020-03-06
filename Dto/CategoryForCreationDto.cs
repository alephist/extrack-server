using System.ComponentModel.DataAnnotations;

namespace ExTrackAPI.Dto
{
    public class CategoryForCreationDto
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
    }
}