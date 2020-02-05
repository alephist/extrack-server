using System.ComponentModel.DataAnnotations;

namespace ExTrackAPI.Dto
{
    public class CategoryForUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}