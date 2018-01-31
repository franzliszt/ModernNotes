using ModernNotes.Client.Model;
using ModernNotes.Client.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModernNotesClient.Controller
{
    public class ModernNotesClientController
    {   
        // POST IT
        public bool PostNewNote(NoteRequest note)
        {
            var restClient = new RestClient(new Uri(Properties.Settings.Default.ModernNotesBaseUri));

            var resource = "api/notes/save";
            var request = new RestRequest(resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(note);

            var response = restClient.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            return true;
        }

        // PUT IT
        public List<Note> UpdateNote(NoteRequest note, int id)
        {
            var restClient = new RestClient(new Uri(Properties.Settings.Default.ModernNotesBaseUri));

            var resource = $"api/notes/updatenote/{id}";
            var request = new RestRequest(resource, Method.PUT);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddBody(note);

            var response = restClient.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return new List<Note>();

            return GetNotes();
        }

        // GET IT
        public List<Note> GetNotes()
        {
            var restClient = new RestClient(new Uri(Properties.Settings.Default.ModernNotesBaseUri));

            var resource = "api/notes/getnotes";
            var request = new RestRequest(resource, Method.GET);

            var response = restClient.Execute<List<Note>>(request);
            if (response.StatusCode != HttpStatusCode.OK)
                return new List<Note>();

            var notes = JsonConvert.DeserializeObject<List<Note>>(response.Content) ?? new List<Note>();
            return notes;
        }

        // DELETE IT
        public List<Note> DeleteNote(int id)
        {
            var restClient = new RestClient(new Uri(Properties.Settings.Default.ModernNotesBaseUri));
            
            var request = new RestRequest($"api/notes/note/{id}", Method.DELETE);

            var response = restClient.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return GetNotes();
            
        }
    }
}
