using ModernNotes.Client.Model;
using System;
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
        private TextBox _text;

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

            var gridCol0 = new ColumnDefinition();
            var gridCol1 = new ColumnDefinition();
            var gridCol2 = new ColumnDefinition();

            grid.ColumnDefinitions.Add(gridCol0);
            grid.ColumnDefinitions.Add(gridCol1);
            grid.ColumnDefinitions.Add(gridCol2);

            var rowNumber = 0;
            _notes.ForEach(note =>
            {
                var row = new RowDefinition();
                grid.RowDefinitions.Add(row);

                _text = new TextBox { Text = note.Timestamp + "\n" + note.NoteText, Name = "textBox" };
                var deleteBtn = new Button
                {
                    Content = "Delete Note",
                    CommandParameter = note.Id,
                };
                deleteBtn.Click += DeleteBtn_Click;
                var updateBtn = new Button
                {
                    Content = "Update Note",
                    CommandParameter = note.Id
                };
                updateBtn.Click += UpdateBtn_Click;

                Grid.SetRow(_text, rowNumber);
                Grid.SetColumn(_text, 0);
                Grid.SetRow(deleteBtn, rowNumber);
                Grid.SetColumn(deleteBtn, 1);
                Grid.SetRow(updateBtn, rowNumber);
                Grid.SetColumn(updateBtn, 2);

                grid.Children.Add(_text);
                grid.Children.Add(deleteBtn);
                grid.Children.Add(updateBtn);

                rowNumber++; // Next row.
            });
            scroll.Content = grid;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).CommandParameter;
            
            _notes = _controller.DeleteNote((int)id);
            
            SetupView();
        }
        
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).CommandParameter;

            var updateText = _text.Text;
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
