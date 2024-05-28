using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.DTOs {
    public class GroupDTO {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(100, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public ICollection<UserGroupDTO> UserGroups { get; set; }

        public ICollection<TaskWorkDTO> TaskWorks { get; set; }

        [Display(Name = "ImageFullPath")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"http://localhost:5157/images/noimage.png"
            : $"/images/groups/{ImageId}.png";
    }
}