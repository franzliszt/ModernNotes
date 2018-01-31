using ModernNotes.Core.Model;
using ModernNotes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernNotes.Core.Interface
{
    public interface IModernNotesRepository
    {
        bool SaveNote(NoteRequest note);
        List<Note> GetNotes();
        bool DeleteNote(int id);
        bool UpdateNote(NoteRequest note, int id);
    }
}
