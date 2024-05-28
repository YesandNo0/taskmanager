using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models {
    public class AddTaskViewModel {
        public int GroupId { get; set; }

        [Display(Name = "Task")]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(1000, ErrorMessage = "The {0} cannot exceed {1} characters")]
        public string Text { get; set; }

        [Display(Name = "FinishDate")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "The end date of the task is required")]
        public DateTime FinishDate { get; set; } = DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
    }
}