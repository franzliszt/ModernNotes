using ModernNotes.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernNotes.Core.Model;

namespace ModernNotes.Core.DataAccess
{
    public class DatabaseInMemory
    {
        private static List<Note> _notes = new List<Note>();

        public bool SaveNote(Note note)
        {
            _notes.Add(note);
            return true;
        }

        public List<Note> GetNotes()
        {
            var allNotes = new List<Note>();
            _notes.ForEach(note => allNotes.Add(note));

            return allNotes;
        }

        public bool DeleteNote(int id)
        {
            Note existingNote = _notes.Find(x => x.Id == id);
            if (existingNote == null)
                return false;
            
            _notes.Remove(existingNote);
            return true;
        }

        public bool UpdateNote(Note note)
        {
            Note existingNote = _notes.Find(x => x.Id == note.Id);

            if (existingNote == null)
                return false;

            existingNote.NoteText = note.NoteText;
            return true;
        }
    }
}
