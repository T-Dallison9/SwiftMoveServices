using System.ComponentModel.DataAnnotations;

namespace SwiftMoveServices.Models
{
    public class ServiceModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "ERROR: Service Name is Required.")]
        [StringLength(50, ErrorMessage = "The product name cannot exceed 50 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ERROR: Service Price is Required")]
        public double Price { get; set; }
        [Required(ErrorMessage = "ERROR: Service Staff is Required")]
        public double Staff { get; set; }
        [Required(ErrorMessage = "ERROR: Service Description is Required")]
        public string Description { get; set; }
    }
} //No references, create new controllers?