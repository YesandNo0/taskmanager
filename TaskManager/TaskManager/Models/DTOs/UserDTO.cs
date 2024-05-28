using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.DTOs {
    public class UserDTO {
        public int Id { get; set; }

        [Display(Name = "Email")]
        [MaxLength(256, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Email { get; set; }

        [MaxLength(256, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        public string NormalizedEmail { get; set; }

        [Display(Name = "Username")]
        [MaxLength(256, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string UserName { get; set; }

        [MaxLength(256, ErrorMessage = "Field {0} must have a maximum of {1} characters.")]
        public string NormalizedUserName { get; set; }

        [Display(Name = "Name")]
        [MaxLength(60, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [MaxLength(120, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string LastName { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<NoteDTO> Notes { get; set; }

        public ICollection<UserGroupDTO> UserGroups { get; set; }

        [Display(Name = "ImageFullPath")]
        public string ImageFullPath => ImageId == Guid.Empty
        ? "/images/noimage.png"
        : $"/images/users/{ImageId}.png";

        [Display(Name = "User")]
        public string FullName =>
            $"{FirstName} {LastName}";
    }
}