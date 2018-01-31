using ModernNotes.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernNotes.Core.Models;
using ModernNotes.Core.Model;

namespace ModernNotes.Core.DataAccess.InMemoryDatabase
{
    public class ModernNotesRepositoryImpl : IModernNotesRepository
    {
        private static int _id = 1;
        private DatabaseInMemory _db;

        public ModernNotesRepositoryImpl()
        {
            _db = new DatabaseInMemory();
        }

        public bool SaveNote(NoteRequest note)
        {
            return _db.SaveNote(new Note
            {
                Id = _id++,
                NoteText = note.Note,
                Timestamp = DateTime.Now
            });
        }

        public List<Note> GetNotes()
        {
            return _db.GetNotes();
        }

        public bool DeleteNote(int id)
        {
            return _db.DeleteNote(id);
        }

        public bool UpdateNote(NoteRequest note, int id)
        {
            var newNote = new Note { NoteText = note.Note, Id = id };
            return _db.UpdateNote(newNote);
        }
    }
}
