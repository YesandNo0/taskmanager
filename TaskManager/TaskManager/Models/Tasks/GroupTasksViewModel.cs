using System.ComponentModel.DataAnnotations;
using TaskManager.Data;

namespace TaskManager.Models {
    public class GroupTasksViewModel {
        public int GroupId { get; set; }

        [Display(Name = "Group")]
        public string Name { get; set; }

        [Display(Name = "ImageFullPath")]
        public string ImageFullPath { get; set; }

        [Display(Name = "Tasks")]
        public IEnumerable<TaskWork> Tasks { get; set; }
    }
}