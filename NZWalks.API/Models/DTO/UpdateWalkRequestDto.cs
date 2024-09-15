using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {


        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be less than 100 characters")]
        [MinLength(5, ErrorMessage = "Name has to be at least 5 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "Description has to be less than 500 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "Lenght has to be between 0 and 1000 km")]
        public double LenghtInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }

    }
}
