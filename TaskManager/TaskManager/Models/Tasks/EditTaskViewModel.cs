using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models {
    public class EditTaskViewModel : AddTaskViewModel {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime FinishDate { get; set; }

    }
}