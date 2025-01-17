﻿using TaskManager.Data;

namespace TaskManager.Services.Interfaces {
    public interface INotesRepository {
        Task<bool> Add(Note note);
        Task<bool> Complete(Note note);
        Task<bool> Delete(Note note);
        Task<bool> Edit(Note note);
        Task<Note> Get(int id);
        Task<bool> UnComplete(Note note);
    }
}