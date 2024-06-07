using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IskoRecords
{
    public partial class ViewStudentPage : Page
    {
        private const string filePath = "iskorecords.txt";
        private List<Student> _students;

        public ViewStudentPage()
        {
            InitializeComponent();
            LoadDataFromFile(); // Call the method to load data when the page is initialized
        }

        private void LoadDataFromFile()
        {
            var students = new List<Student>();

            if (File.Exists(filePath))
            {
                List<string> lines = File.ReadAllLines(filePath).ToList();
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    students.Add(new Student
                    {
                        StudentID = parts[0],
                        FirstName = parts[1],
                        MiddleName = parts[2],
                        LastName = parts[3],
                        YearLevel = int.Parse(parts[4]),
                        Section = parts[5]
                    });
                }
            }

            _students = students; // Store the loaded data in the _students field
            student_table1.ItemsSource = _students; // Set the ItemsSource of the DataGrid
        }

        private void RefreshStudents()
        {
            LoadDataFromFile(); // Refresh the data by reloading from the file
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshStudents();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (student_table1.SelectedItem != null)
            {
                Student selectedStudent = student_table1.SelectedItem as Student;
                if (selectedStudent != null)
                {
                    NavigationService.Navigate(new EditandDeletePage(selectedStudent));
                }
            }
            else
            {
                MessageBox.Show("Please select a student to edit.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void student_table1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Add your event handling logic here
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection changed event
            // No additional code needed for now
        }
    }

    public class Student
    {
        public string StudentID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int YearLevel { get; set; }
        public string Section { get; set; }
    }
}
