﻿using TaskManager.Data;

namespace TaskManager.Services.Interfaces {
    public interface IGroupsRepository {
        Task<bool> Add(Group group, User user);
        Task<bool> DeleteGroup(int groupId);
        Task<bool> Enter(int groupId, User user);
        Task<Group> GetGroup(int groupId);
        Task<IEnumerable<Group>> GetGroups(int userId);
        Task<bool> IsUserInGroup(int groupId, User user);
        Task<bool> UpdateGroup(Group group);
    }
}