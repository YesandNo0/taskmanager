using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models {
    public class EditNoteViewModel : AddNoteViewModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "Note text is required")]
        public string Text { get; set; }

        /*[Display(Name = "State")]
        [Range(0, 3, ErrorMessage = "You must select a state")]
        public int State { get; set; }

        [Display(Name = "States")]
        public IEnumerable<SelectListItem> States { get; set; }*/
    }
}