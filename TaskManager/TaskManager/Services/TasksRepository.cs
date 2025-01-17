﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Enums;
using TaskManager.Services.Interfaces;

namespace TaskManager.Services {
    public class TasksRepository : ITasksRepository  {
        private readonly TaskManagerContext _context;

        public TasksRepository(
            TaskManagerContext context
        ) {
            _context = context;
        }

        public async Task<IEnumerable<TaskWork>> GetAllTasks(int groupId) {
            return await _context.TaskWorks
                .Where(t => t.GroupId == groupId)
                .Include(t => t.Group)
                .OrderBy(t => t.State)
                .ThenByDescending(t => t.StartDate)
                .ThenByDescending(t => t.EndDate)
                .ToListAsync();
        }

        public async Task<TaskWork> GetTask(int taskId) {
            return await _context.TaskWorks
                .Where(t => t.Id == taskId)
                .Include(t => t.Group)
                .FirstAsync();
        }

        public async Task<bool> AddTask(TaskWork task) {
            try {
                await _context.TaskWorks.AddAsync(task);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> EditTask(TaskWork task) {
            try {
                var searchedTask = await _context.TaskWorks
                    .Where(t => t.Id == task.Id)
                    .FirstOrDefaultAsync();

                if (searchedTask is null) {
                    return false;
                }

                searchedTask.Text = task.Text;
                searchedTask.State = task.State;
                searchedTask.EndDate = task.EndDate;

                _context.Entry(searchedTask).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> Complete(TaskWork task) {
            return await Modified(task, true);
        }

        public async Task<bool> UnComplete(TaskWork task) {
            return await Modified(task, false);
        }

        public async Task<bool> Delete(TaskWork task) {
            try {
                var searchedTask = await _context.TaskWorks
                    .Where(t => t.Id == task.Id)
                    .FirstAsync();

                _context.TaskWorks.Remove(searchedTask);
                await _context.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }

        private async Task<bool> Modified(TaskWork task, bool complete) {
            try {
                var searchedTask = await _context.TaskWorks
                    .Where(t => t.Id == task.Id)
                    .FirstAsync();

                searchedTask.State = complete 
                    ? (int)StateTask.Finished
                    : (int)StateTask.Active;

                _context.Entry(searchedTask).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}