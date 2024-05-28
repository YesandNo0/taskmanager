using TaskManager.Data;
using TaskManager.Models.DTOs;

namespace TaskManager.Models {
    public class GroupsNotesViewModel {
        public string Charge { get; set; }

        public IEnumerable<Note> Notes { get; set; }
        
        public IEnumerable<GroupDTO> Groups { get; set; }
        
        public IEnumerable<UserGroup> UserGroups { get; set; }
    }
}