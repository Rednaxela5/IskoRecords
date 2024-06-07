using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace IskoRecords
{
    public partial class AddStudentPage : Page
    {
        public class Student
        {
            public int StudentId { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string YearLevel { get; set; }
            public string Section { get; set; }
        }

        private List<Student> _students;
        private const string FilePath = "iskorecords.txt";

        public AddStudentPage()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            _students = LoadFromFile();
        }

        private void SaveStudents()
        {
            SaveToFile(_students);
        }

        private void student_id_entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Ensure student_id_entry contains only numbers
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string text = textBox.Text;
                if (!Regex.IsMatch(text, @"^\d*$"))
                {
                    textBox.Text = new string(text.Where(char.IsDigit).ToArray());
                    textBox.CaretIndex = textBox.Text.Length; // Move cursor to the end
                }
            }
        }

        private void name_entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Ensure name entries are within 100 characters
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length > 100)
            {
                textBox.Text = textBox.Text.Substring(0, 100);
                textBox.CaretIndex = textBox.Text.Length; // Move cursor to the end
            }
        }

        private void submit_btn_clicked(object sender, RoutedEventArgs e)
        {
            // Retrieve input values
            string StudentId = student_id_entry.Text;
            string FirstName = firstname_entry.Text;
            string MiddleName = middlename_entry.Text;
            string LastName = lastname_entry.Text;
            string YearLevel = (year_combo.SelectedItem as ComboBoxItem)?.Content.ToString();
            string Section = (section_combo.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Validate inputs
            if (string.IsNullOrWhiteSpace(StudentId) || !int.TryParse(StudentId, out int studentId))
            {
                MessageBox.Show("Student ID must be a valid number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (FirstName.Length > 100 || MiddleName.Length > 100 || LastName.Length > 100)
            {
                MessageBox.Show("First name, middle name, and last name must each be 100 characters or fewer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(YearLevel) || string.IsNullOrWhiteSpace(Section))
            {
                MessageBox.Show("Please select a year level and a section.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create Student object
            Student newStudent = new Student
            {
                StudentId = int.Parse(StudentId),
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                YearLevel = YearLevel,
                Section = Section
            };

            // Show confirmation dialog
            MessageBoxResult result = MessageBox.Show("Do you want to save this student data?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Save to file
                try
                {
                    using (StreamWriter writer = new StreamWriter(FilePath, true))
                    {
                        string studentData = $"{newStudent.StudentId},{newStudent.FirstName},{newStudent.MiddleName},{newStudent.LastName},{newStudent.YearLevel},{newStudent.Section}";
                        writer.WriteLine(studentData);
                    }

                    MessageBox.Show("Student data saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving the data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void clear_btn_clicked(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private static void SaveToFile(List<Student> students)
        {
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (Student student in students)
                {
                    string studentData = $"{student.StudentId},{student.FirstName},{student.MiddleName},{student.LastName},{student.YearLevel},{student.Section}";
                    writer.WriteLine(studentData);
                }
            }
        }

        private static List<Student> LoadFromFile()
        {
            var students = new List<Student>();
            if (File.Exists(FilePath))
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 6)
                        {
                            Student student = new Student
                            {
                                StudentId = int.Parse(parts[0]),
                                FirstName = parts[1],
                                MiddleName = parts[2],
                                LastName = parts[3],
                                YearLevel = parts[4],
                                Section = parts[5]
                            };
                            students.Add(student);
                        }
                    }
                }
            }
            return students;
        }

        private void ClearForm()
        {
            student_id_entry.Clear();
            firstname_entry.Clear();
            middlename_entry.Clear();
            lastname_entry.Clear();
            year_combo.SelectedIndex = -1;
            section_combo.SelectedIndex = -1;
        }
    }
}
