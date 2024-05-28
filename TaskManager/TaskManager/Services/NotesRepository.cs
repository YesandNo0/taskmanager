using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Enums;
using TaskManager.Services.Interfaces;

namespace TaskManager.Services {
    public class NotesRepository : INotesRepository {
        private readonly TaskManagerContext _context;

        public NotesRepository(TaskManagerContext context) {
            _context = context;
        }

        public async Task<Note> Get(int id) {
            return await _context.Notes
                .Where(n => n.Id == id)
                .Include(n => n.User)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Add(Note note) {
            try {
                await _context.Notes.AddAsync(note);
                await _context.SaveChangesAsync();
                return true;
            } catch(Exception) {
                return false;
            }
        }

        public async Task<bool> Edit(Note note) {
            try {
                var searchedNote = await SearchNote(note.Id);
                
                if (searchedNote != null) {
                    searchedNote.Text = note.Text;
/*                    searchedNote.State = note.State;
*/                    searchedNote.Date = DateTime.Now;

                    _context.Entry(searchedNote).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return true;
                } else {
                    return false;
                }
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> Complete(Note note) {
            try {
                var band = await Modified(
                    await SearchNote(note.Id),
                    (int)StateTask.Finished
                );

                return band;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> UnComplete(Note note) {
            try {
                var band = await Modified(
                    await SearchNote(note.Id),
                    (int)StateTask.Active
                );

                return band;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> Delete(Note note) {
            try {
                var searchedNote = await SearchNote(note.Id);
                _context.Notes.Remove(searchedNote);
                await _context.SaveChangesAsync();

                return true;
            } catch(Exception) {
                return false;
            }
        }

        public async Task<bool> Modified(Note note, int newState) {
            try {
                note.State = newState;
                _context.Entry(note).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        } 

        private async Task<Note> SearchNote(int id) {
            return await _context.Notes
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}