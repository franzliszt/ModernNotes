using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernNotes.Core.DataAccess;
using ModernNotes.Core.Interface;
using ModernNotes.Core.Model;
using ModernNotes.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernNotes.Core.Tests.DataAccess
{
    [TestClass]
    public class ModernNotesRepositoryTests
    {
        [TestMethod]
        public void DeleteNoteOk()
        {
            // arrange
            var assertCount = 2;

            var db = new List<Note>
            {
                new Note {Id = 1, NoteText = "Note 1", Timestamp = DateTime.Now },
                new Note {Id = 2, NoteText = "Note 2", Timestamp = DateTime.Now },
                new Note {Id = 3, NoteText = "Note 3", Timestamp = DateTime.Now }
            };

            var mock = new Mock<IModernNotesRepository>();
            mock.Setup(x => x.DeleteNote(It.IsAny<int>()))
                .Callback((int id) =>
                {
                    var note = db.Find(x => x.Id == id);
                    db.Remove(note);
                }).Returns(true);

            // act
            var result = new ModernNotesRepository(mock.Object).DeleteNote(1);

            // assert
            mock.Verify(x => x.DeleteNote(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(assertCount, db.Count);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetNotes()
        {
            // arrange
            var assertCount = 2;

            var mock = new Mock<IModernNotesRepository>();
            mock.Setup(x => x.GetNotes()).
                Returns(() => new List<Note>
                {
                    new Note {Id = 1, NoteText = "NoteText", Timestamp = DateTime.Now },
                    new Note {Id = 2, NoteText = "NoteText", Timestamp = DateTime.Now },
                });

            // act
            var notes = new ModernNotesRepository(mock.Object).GetNotes();

            // assert
            mock.Verify(x => x.GetNotes(), Times.Once);
            Assert.AreEqual(assertCount, notes.Count);
        }

        [TestMethod]
        public void SaveNoteOk()
        {
            // arrange
            var assertNoteText = "Initial version";

            var note = new NoteRequest { Note = "Initial version" };

            var db = new List<Note>();

            var mock = new Mock<IModernNotesRepository>();
            mock.Setup(x => x.SaveNote(It.IsAny<NoteRequest>()))
                .Callback((NoteRequest req) =>
                {
                    db.Add(new Note
                    {
                        Id = 1,
                        NoteText = "Initial version",
                        Timestamp = DateTime.Now
                    });
                }).Returns(true);

            // act
            bool result = new ModernNotesRepository(mock.Object).SaveNote(note);

            // assert
            mock.Verify(x => x.SaveNote(It.IsAny<NoteRequest>()), Times.Once);
            Assert.IsTrue(result);
            Assert.IsTrue(db.Count == 1);
            Assert.AreEqual(assertNoteText, db.Last().NoteText);
        }

        [TestMethod]
        public void UpdateNoteOk()
        {
            // arrange
            var assertNoteText = "Updated";

            var db = new List<Note>
            {
                new Note {Id = 1, NoteText = "Note 1", Timestamp = DateTime.Now },
                new Note {Id = 2, NoteText = "Note 2", Timestamp = DateTime.Now },
                new Note {Id = 3, NoteText = "Note 3", Timestamp = DateTime.Now }
            };
            var noteWithUpdate = new NoteRequest { Note = "Updated" };

            var mock = new Mock<IModernNotesRepository>();
            mock.Setup(x => x.UpdateNote(It.IsAny<NoteRequest>(), It.IsAny<int>()))
                .Callback((NoteRequest req, int id) =>
                {
                    Note noteToBeUpdated = db.Find(x => x.Id == id);
                    noteToBeUpdated.NoteText = req.Note;
                }).Returns(true);

            // act
            var result = new ModernNotesRepository(mock.Object).UpdateNote(noteWithUpdate, 1);

            // assert
            mock.Verify(x => x.UpdateNote(It.IsAny<NoteRequest>(), It.IsAny<int>()), Times.Once);
            Assert.IsTrue(result);
            Assert.AreEqual(assertNoteText, db.Find(x => x.Id == 1).NoteText);
        }
    }
}
