using ModernNotes.Client.Models;
using ModernNotesClient.Controller;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModernNotesClient
{
    /// <summary>
    /// Interaction logic for AddNewNote.xaml
    /// </summary>
    public partial class AddNewNote : Page
    {
        private readonly string _baseUri = "http://localhost:51382/";

        public AddNewNote()
        {
            InitializeComponent();
            textBox.Clear();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var note = new NoteRequest(textBox.Text);

            var isSaved = new ModernNotesClientController().PostNewNote(note);
            if (isSaved)
                this.NavigationService.Navigate(new ViewMyNotes());
            
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = string.Empty;
        }

        private void AllNotesButton_Click(object sender, RoutedEventArgs e)
        {
            ViewMyNotes notes = new ViewMyNotes();
            this.NavigationService.Navigate(notes);
        }
    }
}
