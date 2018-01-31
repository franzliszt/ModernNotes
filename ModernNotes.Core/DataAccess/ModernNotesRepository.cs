using ModernNotes.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernNotes.Core.Models;
using ModernNotes.Core.DataAccess.InMemoryDatabase;
using ModernNotes.Core.Model;

namespace ModernNotes.Core.DataAccess
{
    public class ModernNotesRepository : IModernNotesRepository
    {
        private IModernNotesRepository _repo;

        public ModernNotesRepository(IModernNotesRepository repo = null)
        {
            _repo = repo ?? new ModernNotesRepositoryImpl();
        }

        public bool DeleteNote(int id)
        {
            return _repo.DeleteNote(id);
        }

        public List<Note> GetNotes()
        {
            return _repo.GetNotes();
        }

        public bool SaveNote(NoteRequest note)
        {
            return _repo.SaveNote(note);
        }

        public bool UpdateNote(NoteRequest note, int id)
        {
            return _repo.UpdateNote(note, id);
        }
    }
}
