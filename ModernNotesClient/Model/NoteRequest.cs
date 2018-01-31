using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModernNotes.Client.Models
{
    public class NoteRequest
    {
        public string Note { get; private set; }

        public NoteRequest(string text)
        {
            Note = text;
        }
    }
}