using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models {
    public class CreateGroupViewModel {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "The {0} of the group is required")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        [Display(Name = "ImageFullPath")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"http://localhost:5157/images/noimage.png"
            : $"/images/groups/{ImageId}.png";

        [Display(Name = "ImageFile")]
        public IFormFile ImageFile { get; set; }
    }
}