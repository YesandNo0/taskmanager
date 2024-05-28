using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models {
    public class EditUserViewModel {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [MaxLength(50, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string LastName { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        [Display(Name = "ImageFullPath")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"http://localhost:5157/images/noimage.png"
            : $"/images/users/{ImageId}.png";

        [Display(Name = "ImageFile")]
        public IFormFile ImageFile { get; set; }
    }
}