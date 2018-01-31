using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ModernNotes.Core.Interfaces;
using ModernNotes.Core.Model;
using ModernNotes.Api.Service;
using ModernNotes.Core.Models;

namespace ModernNotes.Api.Controllers
{
    [RoutePrefix("api/notes")]
    public class ModernNotesController : ApiController
    {
        private IModernNotesService _service;

        public ModernNotesController() : this (null)
        { }

        public ModernNotesController(IModernNotesService service = null)
        {
            _service = service ?? new ModernNotesService();
        }

        [HttpPost]
        [Route("save")]
        public IHttpActionResult SaveNote([FromBody] NoteRequest content)
        {
            if (!ModelState.IsValid || content == null)
                return Content(HttpStatusCode.BadRequest, "Not a valid request.");

            bool isSaved = _service.SaveNote(content);
            //if (!isSaved)
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError);

            return Content(HttpStatusCode.OK, isSaved);
        }

        [HttpGet]
        [Route("getnotes")]
        public IHttpActionResult GetNotes()
        {
            var result = _service.GetNotes();

            if (!result.Any())
                return Content(HttpStatusCode.NoContent, result);

            return Content(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("note/{id}")]
        public IHttpActionResult DeleteNote(int id)
        {
            bool isDeleted = _service.DeleteNote(id);

            if (!isDeleted)
                return Content(HttpStatusCode.NotFound, isDeleted);

            return Content(HttpStatusCode.OK, isDeleted);
        }

        [HttpPut]
        [Route("updatenote/{id}")]
        public IHttpActionResult UpdateNote([FromBody] NoteRequest note, int id)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "Not a valid request.");

            bool isUpdated = _service.UpdateNote(note, id);
            if (!isUpdated)
                return Content(HttpStatusCode.NotFound, isUpdated);

            return Content(HttpStatusCode.OK, isUpdated);
        }
    }
}
