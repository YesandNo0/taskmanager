using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Enums;
using TaskManager.Helpers.Interfaces;

namespace TaskManager.Helpers {
    public class SelectListHelper : ISelectListHelper {
        public IEnumerable<SelectListItem> GetStates() => new List<SelectListItem>() {
            new SelectListItem {
                Text = StateTask.Active.ToString(),
                Value = $"{(int)StateTask.Active}"
            },
            new SelectListItem {
                Text = StateTask.Finished.ToString(),
                Value = $"{(int)StateTask.Finished}"
            },
            new SelectListItem {
                Text = StateTask.Cancelled.ToString(),
                Value = $"{(int)StateTask.Cancelled}"
            }
        };
    }
}