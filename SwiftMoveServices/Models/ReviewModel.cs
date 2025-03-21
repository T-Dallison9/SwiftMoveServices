using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftMoveServices.Models
{
    public class ReviewModel
    {
        [Key] //Set the primary key
        public int ReviewId { get; set; }

        [Required(ErrorMessage = "Review title is required.")]
        [MaxLength(50, ErrorMessage = "The title cannot exceed 50 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Review content is required.")]
        [MaxLength(500, ErrorMessage = "The content cannot exceed 500 characters.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Review rating is required.")]
        [Range(1, 5)]
        public int Rating { get; set; }

        //Foreign Key
        [Required]
        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public ServiceModel Service { get; set; }
    }
}