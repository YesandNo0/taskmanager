using System.ComponentModel.DataAnnotations;
using TaskManager.Enums;

namespace TaskManager.Models.DTOs {
    public class UserGroupDTO {
        public int Id { get; set; }

        [Display(Name = "User")]
        public UserDTO User { get; set; }

        [Display(Name = "Group")]
        public GroupDTO Group { get; set; }

        [Display(Name = "State")]
        public StateInGroup State { get; set; }
    }
}