using System.ComponentModel.DataAnnotations;

namespace NZWalk.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be Minimum of 3 character")]
        [MaxLength(3, ErrorMessage = "Code has to be Maximum of 3 character")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be Maximum of 100 character")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
