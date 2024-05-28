using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models {
    public class EnterGroupViewModel {
        [Display(Name = "Code")]
        [Required(ErrorMessage = "{0} is required")]
        public string Code { get; set; }
    }
}