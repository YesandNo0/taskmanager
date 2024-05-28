using System.ComponentModel.DataAnnotations;
using TaskManager.Services;

namespace TaskManager.Models {
    public class EditGroupViewModel {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(100, ErrorMessage = "The {0} must have a maximum of {1} characters")]
        [Required(ErrorMessage = "The {0} is necessary")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public Guid? ImageId { get; set; }

        [Display(Name = "ImageFile")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "ImageFullPath")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"http://localhost:5157/images/noimage.png"
            : $"/images/groups/{ImageId}.png";
    }
}