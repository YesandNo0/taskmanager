using System.ComponentModel.DataAnnotations;
using TaskManager.Enums;

namespace TaskManager.Models.DTOs {
    public class TaskWorkDTO {
        public int Id { get; set; }

        [Display(Name = "Note")]
        [MaxLength(1000, ErrorMessage = "The note cannot exceed {1} characters")]
        public string Text { get; set; }

        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "State")]
        public StateTask State { get; set; }

        [Display(Name = "Group")]
        public GroupDTO Group { get; set; }
    }
}