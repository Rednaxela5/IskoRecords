using System;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Linq;

namespace IskoRecords
{
    public partial class AddStudentPage : Page
    {
        private string connectionString = "server=localhost;user=root;password=P@ssw0rd2023!;database=db_iskorecords;port=3306";

        public class Student
        {
            public int StudentID { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public int YearLevel { get; set; }
            public string Section { get; set; }
        }

        public AddStudentPage()
        {
            InitializeComponent();
        }

        // Event handlers for Student ID
        private void student_id_entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string text = textBox.Text;
                if (!System.Text.RegularExpressions.Regex.IsMatch(text, @"^\d*$"))
                {
                    textBox.Text = new string(text.Where(char.IsDigit).ToArray());
                    textBox.CaretIndex = textBox.Text.Length;
                }
            }
        }

        // Event handlers for Name Entry
        private void name_entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length > 100)
            {
                textBox.Text = textBox.Text.Substring(0, 100);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }


        // Event handlers for Submit and Clear buttons
        private void submit_btn_clicked(object sender, RoutedEventArgs e)
        {
            string StudentId = student_id_entry.Text;
            string FirstName = firstname_entry.Text;
            string MiddleName = middlename_entry.Text;
            string LastName = lastname_entry.Text;
            string YearLevel = (year_combo.SelectedItem as ComboBoxItem)?.Content.ToString();
            string Section = (section_combo.SelectedItem as ComboBoxItem)?.Content.ToString();

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

            Student newStudent = new Student
            {
                StudentID = int.Parse(StudentId),
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                YearLevel = int.Parse(YearLevel),
                Section = Section
            };

            MessageBoxResult result = MessageBox.Show("Do you want to save this student data?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "INSERT INTO tb_student (studentID, firstName, middleName, lastName, yearLevel, section) VALUES (@studentID, @firstName, @middleName, @lastName, @yearLevel, @section)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@studentID", newStudent.StudentID);
                        cmd.Parameters.AddWithValue("@firstName", newStudent.FirstName);
                        cmd.Parameters.AddWithValue("@middleName", newStudent.MiddleName);
                        cmd.Parameters.AddWithValue("@lastName", newStudent.LastName);
                        cmd.Parameters.AddWithValue("@yearLevel", newStudent.YearLevel);
                        cmd.Parameters.AddWithValue("@section", newStudent.Section);
                        cmd.ExecuteNonQuery();
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
