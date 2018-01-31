using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ModernNotes.Core.Interfaces;
using ModernNotes.Api.Controllers;
using System.Net.Http;
using System.Web.Http;
using ModernNotes.Core.Models;
using ModernNotes.Core.Model;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Net;

namespace ModernNotes.Api.Tests
{
    [TestClass]
    public class ModernNotesControllerTests
    {
        [TestMethod]
        public void SaveNoteOk()
        {
            // arrange
            var request = new NoteRequest { Note = "A new note." };

            var mock = new Mock<IModernNotesService>();
            mock.Setup(x => x.SaveNote(It.IsAny<NoteRequest>()))
                .Returns(true);

            var controller = new ModernNotesController(mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // act
            var response = controller.SaveNote(request) as NegotiatedContentResult<bool>;

            // assert
            mock.Verify(x => x.SaveNote(It.IsAny<NoteRequest>()), Times.Once);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void SaveNoteInvalidModelState()
        {
            // arrange
            var controller = new ModernNotesController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            controller.ModelState.AddModelError("key", "Modelstate is not valid.");

            // act
            var response = controller.SaveNote(null) as NegotiatedContentResult<string>;

            // assert
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void GetNotesOk()
        {
            // arrange
            var mock = new Mock<IModernNotesService>();
            mock.Setup(x => x.GetNotes())
                .Returns(() => new List<Note>
                {
                    new Note {Id = 1, NoteText = "Note one.", Timestamp = DateTime.Now},
                    new Note {Id = 2, NoteText = "Note two.", Timestamp = DateTime.Now},
                    new Note {Id = 3, NoteText = "Note three.", Timestamp = DateTime.Now},
                });

            var controller = new ModernNotesController(mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // act
            var response = controller.GetNotes() as NegotiatedContentResult<List<Note>>;

            // assert
            mock.Verify(x => x.GetNotes(), Times.Once);
            Assert.IsNotNull(response);
            Assert.AreEqual(3, response.Content.Count);
        }

        [TestMethod]
        public void GetNotesNotOk()
        {
            // arrange
            var mock = new Mock<IModernNotesService>();
            mock.Setup(x => x.GetNotes()).Returns(new List<Note>());

            var controller = new ModernNotesController(mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // act
            var response = controller.GetNotes() as NegotiatedContentResult<List<Note>>;

            // assert
            mock.Verify(x => x.GetNotes(), Times.Once);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            Assert.AreEqual(0, response.Content.Count);
        }

        [TestMethod]
        public void DeleteNoteOk()
        {
            // arrange
            var mock = new Mock<IModernNotesService>();
            mock.Setup(x => x.DeleteNote(It.IsAny<int>())).Returns(true);

            var controller = new ModernNotesController(mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // act
            var response = controller.DeleteNote(1) as NegotiatedContentResult<bool>;

            // assert
            mock.Verify(x => x.DeleteNote(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.Content);
        }

        [TestMethod]
        public void DeleteNotOk()
        {
            // arrange
            var mock = new Mock<IModernNotesService>();
            mock.Setup(x => x.DeleteNote(It.IsAny<int>())).Returns(false);

            var controller = new ModernNotesController(mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // act
            var response = controller.DeleteNote(-1) as NegotiatedContentResult<bool>;

            // assert
            mock.Verify(x => x.DeleteNote(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.Content);
        }

        [TestMethod]
        public void UpdateNoteOk()
        {
            // arrange
            var newNote = new NoteRequest { Note = "New note." };

            var mock = new Mock<IModernNotesService>();
            mock.Setup(x => x.UpdateNote(It.IsAny<NoteRequest>(), It.IsAny<int>()))
                .Returns(true);

            var controller = new ModernNotesController(mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // act
            var response = controller.UpdateNote(newNote, 1) as NegotiatedContentResult<bool>;

            // assert
            mock.Verify(x => x.UpdateNote(It.IsAny<NoteRequest>(), It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.Content);
        }

        [TestMethod]
        public void UpdateInvalidModelState()
        {
            // arrange
            var controller = new ModernNotesController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            controller.ModelState.AddModelError("key", "Modelstate is not valid.");

            // act
            var response = controller.UpdateNote(null, 1) as NegotiatedContentResult<string>;

            // assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void UpdateNoteNotOk()
        {
            // arrange
            var newNote = new NoteRequest { Note = "New note." };

            var mock = new Mock<IModernNotesService>();
            mock.Setup(x => x.UpdateNote(It.IsAny<NoteRequest>(), It.IsAny<int>()))
                .Returns(false);

            var controller = new ModernNotesController(mock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // act
            var response = controller.UpdateNote(newNote, -1) as NegotiatedContentResult<bool>;

            // assert
            mock.Verify(x => x.UpdateNote(It.IsAny<NoteRequest>(), It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.Content);
        }
    }
}
