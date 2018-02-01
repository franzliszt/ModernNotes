using ModernNotes.Client.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ModernNotesClient.Controller;
using ModernNotes.Client.Models;

namespace ModernNotesClient
{
    /// <summary>
    /// Interaction logic for ViewMyNotes.xaml
    /// </summary>
    public partial class ViewMyNotes : Page
    {
        private readonly ModernNotesClientController _controller;
        private List<Note> _notes;

        public ViewMyNotes()
        {
            InitializeComponent();

            _controller = new ModernNotesClientController();
            _notes = _controller.GetNotes();

            SetupView();
        }

        private void SetupView()
        {
            if (_notes == null)
                _notes = new List<Note>();

            var grid = new Grid();
            var rowNumber = 0;

           _notes.ForEach(note =>
            {
                var row = new RowDefinition();
                grid.RowDefinitions.Add(row);

                var stackpanel = new StackPanel { Orientation = Orientation.Horizontal };
                var text = new TextBox { Text = note.NoteText };

                Button updateButton = CreateUpdateButton(note);
                Button deleteButton = CreateDeleteButton(note);
                
                stackpanel.Children.Add(text);
                stackpanel.Children.Add(updateButton);
                stackpanel.Children.Add(deleteButton);

                Grid.SetRow(stackpanel, rowNumber);
                grid.Children.Add(stackpanel);

                rowNumber++; // Next row.
            });
            scroll.Content = grid;
        }

        private Button CreateDeleteButton(Note note)
        {
            var deleteBtn = new Button
            {
                Content = "Delete Note",
                Tag = note.Id,
            };
            deleteBtn.Click += DeleteBtn_Click;

            return deleteBtn;
        }

        private Button CreateUpdateButton(Note note)
        {
            var updateBtn = new Button
            {
                Content = "Update Note",
                Tag = note.Id
            };
            updateBtn.Click += UpdateBtn_Click;

            return updateBtn;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            _notes = _controller.DeleteNote((int) button.Tag);
            
            SetupView();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var parent = button.Parent as StackPanel;

            if (parent == null)
                return;

            TextBox textBoxChild = parent.Children[0] as TextBox;

            int id = (int)button.Tag;
            var updateText = textBoxChild.Text;

            var request = new NoteRequest(updateText);
            _notes = _controller.UpdateNote(request,(int) id);

            GoToAddNoteView();
        }

        private void GoToAddNoteView()
        {
            this.NavigationService.Navigate(new AddNewNote());
        }
    }
}
