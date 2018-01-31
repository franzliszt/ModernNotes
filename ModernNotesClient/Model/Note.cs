using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernNotes.Client.Model
{
    public class Note
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
