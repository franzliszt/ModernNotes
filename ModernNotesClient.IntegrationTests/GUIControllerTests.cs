using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernNotesClient.Controller;
using ModernNotes.Client.Model;
using System.Collections.Generic;
using ModernNotes.Client.Models;
using System.Linq;

namespace ModernNotesClient.IntegrationTests
{
    [TestClass]
    public class GUIControllerTests
    {
        [TestMethod]
        public void Post()
        {
            // arrange
            var assertNoteText = "A note.";

            var controller = new ModernNotesClientController();
            List<Note> allNotesBeforePost = controller.GetNotes();

            var requestBody = new NoteRequest("A note.");

            // act
            bool response = controller.PostNewNote(requestBody);
            List<Note> allNotesAfterPost = controller.GetNotes();

            var lastSaveNote = allNotesAfterPost.Last();

            // assert
            Assert.AreEqual(allNotesBeforePost.Count + 1, allNotesAfterPost.Count);
            Assert.AreEqual(assertNoteText, lastSaveNote.NoteText);
        }

        [TestMethod]
        public void Get()
        {
            // arrange
            var note = new NoteRequest("A note.");
            var controller = new ModernNotesClientController();
            bool responseFromPost = controller.PostNewNote(note); // If database is empty.

            // act
            List<Note> allNotes = controller.GetNotes();

            // assert
            Assert.IsTrue(responseFromPost);
            Assert.IsTrue(allNotes.Count > 0);
        }

        [TestMethod]
        public void Update()
        {
            // arrange
            var note = new NoteRequest("A note.");
            var assertNoteTextAfterUpdate = "Update";

            var controller = new ModernNotesClientController();
            bool responseFromPost = controller.PostNewNote(note); // If database is empty.
            List<Note> getAllNotesBeforeUpdate = controller.GetNotes();

            Note lastNoteBeforeUpdate = getAllNotesBeforeUpdate.Last();
            int lastNoteBeforeUpdateId = lastNoteBeforeUpdate.Id;

            var updateLastNote = new NoteRequest("Update");

            // act
            List<Note> allNotesAfterUpdate = controller.UpdateNote(updateLastNote, lastNoteBeforeUpdateId);
            Note lastNoteAfterUpdate = allNotesAfterUpdate.Last();

            // assert
            Assert.IsTrue(responseFromPost);
            Assert.AreEqual(getAllNotesBeforeUpdate.Count, allNotesAfterUpdate.Count);
            Assert.AreEqual(assertNoteTextAfterUpdate, lastNoteAfterUpdate.NoteText);
            Assert.AreEqual(lastNoteBeforeUpdateId, lastNoteAfterUpdate.Id);
        }

        [TestMethod]
        public void Delete()
        {
            // arrange
            var note = new NoteRequest("A note.");

            var controller = new ModernNotesClientController();
            bool responseFromPost = controller.PostNewNote(note); // If database is empty.

            List<Note> allNotesBeforeDelete = controller.GetNotes();
            int lastNoteId = allNotesBeforeDelete.Last().Id;

            // act
            List<Note> allNotesAfterDelete = controller.DeleteNote(lastNoteId);

            var result = controller.DeleteNote(lastNoteId); // Check if a note with lastNoteId is gone, should return null if already deleted.

            // assert
            Assert.IsTrue(responseFromPost);
            Assert.AreEqual(allNotesBeforeDelete.Count - 1, allNotesAfterDelete.Count);

            Assert.IsNull(result);
        }
    }
}
