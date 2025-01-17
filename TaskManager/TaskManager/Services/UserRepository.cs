﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Services.Interfaces;

namespace TaskManager.Services {
    public class UserRepository : IUserRepository {
        private readonly TaskManagerContext _context;

        public UserRepository(TaskManagerContext context) {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync() {
            return await _context.Users
                .ToListAsync();
        }

        public async Task<User> GetUserAsync(int id) {
            return await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Notes)
                .Include(u => u.UserGroups)
                .ThenInclude(ug => ug.Group)
                .ThenInclude(g => g.TaskWorks)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateUserAsync(User user) {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var userInStore = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            return userInStore.Id;
        }

        public async Task UpdateUserAsync(User user) {
            var updatedUser = await _context.Users
                .Where(u => u.Id == user.Id)
                .FirstAsync();

            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.ImageId = user.ImageId;

            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user) {
            var deletedUser = await _context.Users
                .Where(u => u.Id == user.Id)
                .FirstAsync();

            _context.Users.Remove(deletedUser);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(int id) {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email) {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.NormalizedEmail == email);
        }

        public async Task<User> GetByUserNameAsync(string username) {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.NormalizedUserName == username);
        }
    }
}