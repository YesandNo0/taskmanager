using System.ComponentModel.DataAnnotations;
using TaskManager.Enums;

namespace TaskManager.Models.DTOs {
    public class NoteDTO {
        public int Id { get; set; }

        [Display(Name = "Note")]
        [MaxLength(1000, ErrorMessage = "The note cannot exceed {1} characters")]
        public string Text { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "State")]
        public StateTask State { get; set; }

        [Display(Name = "User")]
        public UserDTO User { get; set; }
    }
}