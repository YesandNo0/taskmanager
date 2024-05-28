using System.ComponentModel.DataAnnotations;
using TaskManager.Models.DTOs;

namespace TaskManager.Models {
    public class UserDetailsViewModel {
        [Display(Name = "Email")]
        [MaxLength(256, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [MaxLength(256, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string UserName { get; set; }

        [Display(Name = "Name")]
        [MaxLength(60, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [MaxLength(120, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string LastName { get; set; }

        [Display(Name = "Image")]
        public Guid? ImageId { get; set; }

        public ICollection<NoteDTO> Notes { get; set; }

        public ICollection<UserGroupDTO> UserGroups { get; set; }

        [Display(Name = "ImageFullPath")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"http://localhost:5157/images/noimage.png"
            : $"/images/users/{ImageId}.png";

        [Display(Name = "User")]
        public string FullName =>
            $"{FirstName} {LastName}";

        public int NotesNumber =>
            Notes == null ? 0 : Notes.Count;

        public int GroupsNumber =>
            UserGroups == null ? 0 : UserGroups.Count;

        public int TasksNumbers => 
            UserGroups == null 
            ? 0 : UserGroups.Sum(ug => ug.Group.TaskWorks.Count);
    }
}