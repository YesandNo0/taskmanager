using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManager.Helpers.Interfaces {
    public interface ISelectListHelper {
        IEnumerable<SelectListItem> GetStates();
    }
}