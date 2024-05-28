using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models {
    public class DetailGroupViewModel {
        public int GroupId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "ImageFullPath")]
        public string ImageFullPath { get; set; }
    }
}