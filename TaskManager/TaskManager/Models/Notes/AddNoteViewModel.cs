using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models {
    public class AddNoteViewModel {
        [Display(Name = "Note: ")]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(1000, ErrorMessage = "The note cannot exceed {1} characters")]
        public string Text { get; set; }
    }
}