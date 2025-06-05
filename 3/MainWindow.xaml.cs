using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;


namespace NotesApp
{
    public partial class MainWindow : Window
    {
        private const string FilePath = "notes.txt";
        private List<string> notes = new List<string>();
        private int selectedIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            LoadNotes();
        }

        private void LoadNotes()
        {
            if (File.Exists(FilePath))
            {
                notes = File.ReadAllLines(FilePath).ToList();
                NotesList.ItemsSource = notes;
            }
        }

        private void SaveNotes()
        {
            File.WriteAllLines(FilePath, notes);
        }

        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NoteInput.Text))
            {
                notes.Add(NoteInput.Text);
                RefreshList();
                NoteInput.Clear();
            }
        }

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIndex >= 0 && selectedIndex < notes.Count)
            {
                notes[selectedIndex] = NoteInput.Text;
                RefreshList();
            }
        }

        private void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIndex >= 0 && selectedIndex < notes.Count)
            {
                notes.RemoveAt(selectedIndex);
                NoteInput.Clear();
                RefreshList();
            }
        }

        private void RefreshList()
        {
            NotesList.ItemsSource = null;
            NotesList.ItemsSource = notes;
            SaveNotes();
        }

        private void NotesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedIndex = NotesList.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < notes.Count)
            {
                NoteInput.Text = notes[selectedIndex];
            }
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var filtered = notes.Where(n => n.Contains(SearchBox.Text, StringComparison.OrdinalIgnoreCase)).ToList();
            NotesList.ItemsSource = filtered;
        }
    }
}

