using System.ComponentModel.DataAnnotations;

namespace ExTrackAPI.Dto
{
    public class CategoryForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}