using ModernNotes.Core.Model;
using ModernNotes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernNotes.Core.Interfaces
{
    public interface IModernNotesService
    {
        bool SaveNote(NoteRequest note);
        List<Note> GetNotes();
        bool UpdateNote(NoteRequest note, int id);
        bool DeleteNote(int id);
    }
}
