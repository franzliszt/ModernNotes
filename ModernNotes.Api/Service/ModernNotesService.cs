using ModernNotes.Core.Interfaces;
using System;
using ModernNotes.Core.Models;
using ModernNotes.Core.Interface;
using ModernNotes.Core.DataAccess;
using ModernNotes.Core.Model;
using System.Collections.Generic;

namespace ModernNotes.Api.Service
{
    public class ModernNotesService : IModernNotesService
    {
        private IModernNotesRepository _repo;

        public ModernNotesService() : this (null)
        { }

        public ModernNotesService(IModernNotesRepository service = null)
        {
            _repo = service ?? new ModernNotesRepository();
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
            _repo.SaveNote(note);
            return true;
        }

        public bool UpdateNote(NoteRequest note, int id)
        {
            return _repo.UpdateNote(note, id);
        }
    }
}