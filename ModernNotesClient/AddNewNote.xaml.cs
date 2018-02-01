using ModernNotes.Client.Models;
using ModernNotesClient.Controller;
using System.Windows;
using System.Windows.Controls;

namespace ModernNotesClient
{
    /// <summary>
    /// Interaction logic for AddNewNote.xaml
    /// </summary>
    public partial class AddNewNote : Page
    {
        public AddNewNote()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var note = new NoteRequest(textBox.Text);
            var isSaved = new ModernNotesClientController().PostNewNote(note);
            if (isSaved)
            {
                textBox.Clear();
                this.NavigationService.Navigate(new ViewMyNotes());
            }
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
