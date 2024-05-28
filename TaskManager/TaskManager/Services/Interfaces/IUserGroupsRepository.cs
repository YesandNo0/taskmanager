using TaskManager.Data;

namespace TaskManager.Services.Interfaces {
    public interface IUserGroupsRepository {
        Task<bool> DeleteUserGroupAsync(int userGroupId);
        Task<UserGroup> GetUserGroupAsync(int userId, int groupId);
        Task<IEnumerable<UserGroup>> GetUsersGroupsAsync(int userId);
    }
}