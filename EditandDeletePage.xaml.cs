using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace IskoRecords
{
    public partial class EditandDeletePage : Page
    {
        private Student _student;
        private const string filePath = "iskorecords.txt";

        public EditandDeletePage(Student student)
        {
            InitializeComponent();
            _student = student;
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            student_id_entry.Text = _student.StudentID;
            firstname_entry.Text = _student.FirstName;
            middlename_entry.Text = _student.MiddleName;
            lastname_entry.Text = _student.LastName;
            year_combo.Text = _student.YearLevel.ToString();
            section_combo.Text = _student.Section;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _student.FirstName = firstname_entry.Text;
            _student.MiddleName = middlename_entry.Text;
            _student.LastName = lastname_entry.Text;
            _student.YearLevel = int.Parse(year_combo.Text);
            _student.Section = section_combo.Text;

            List<string> lines = File.ReadAllLines(filePath).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts[0] == _student.StudentID)
                {
                    lines[i] = $"{_student.StudentID},{_student.FirstName},{_student.MiddleName},{_student.LastName},{_student.YearLevel},{_student.Section}";
                    break;
                }
            }
            File.WriteAllLines(filePath, lines);
            MessageBox.Show("Student data saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            lines.RemoveAll(line => line.StartsWith(_student.StudentID));
            File.WriteAllLines(filePath, lines);
            MessageBox.Show("Student data deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }

        private void find_btn_clicked(object sender, RoutedEventArgs e)
        {
            // Implement find functionality if needed
        }

        private void clear_btn_clicked(object sender, RoutedEventArgs e)
        {
            // Clear all textboxes
            student_id_entry.Clear();
            firstname_entry.Clear();
            middlename_entry.Clear();
            lastname_entry.Clear();
            year_combo.SelectedIndex = -1;
            section_combo.SelectedIndex = -1;
        }
    }
}
