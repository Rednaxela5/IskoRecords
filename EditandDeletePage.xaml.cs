using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using static IskoRecords.AddStudentPage;

namespace IskoRecords
{
    public partial class EditandDeletePage : Page
    {
        private ViewStudentPage.Student _selectedStudent;
        private MySqlConnection _connection;

        public EditandDeletePage(ViewStudentPage.Student selectedStudent, MySqlConnection connection)
        {
            InitializeComponent();
            _selectedStudent = selectedStudent;
            _connection = connection;
            DisplayIskoRecords();
        }

        private void DisplayIskoRecords()
        {
            student_id_entry.Text = _selectedStudent.StudentID;
            firstname_entry.Text = _selectedStudent.FirstName;
            middlename_entry.Text = _selectedStudent.MiddleName;
            lastname_entry.Text = _selectedStudent.LastName;
            year_combo.Text = _selectedStudent.YearLevel.ToString();
            section_combo.Text = _selectedStudent.Section;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Variables for student data
            string firstName = firstname_entry.Text;
            string middleName = middlename_entry.Text;
            string lastName = lastname_entry.Text;

            if (!ValidateNameLength(firstName) || !ValidateNameLength(middleName) || !ValidateNameLength(lastName))
            {
                MessageBox.Show("Please make sure names are less than or equal to 100 characters.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _connection.Open();

            try
            {
                string query = "UPDATE tb_student SET firstName = @firstName, middleName = @middleName, lastName = @lastName, yearLevel = @yearLevel, section = @section WHERE studentID = @studentID";
                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@middleName", middleName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@yearLevel", Convert.ToInt32(year_combo.Text));
                    command.Parameters.AddWithValue("@section", section_combo.Text);
                    command.Parameters.AddWithValue("@studentID", _selectedStudent.StudentID);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Student data updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating student data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _connection.Close();
            }   

        }



        // Delete student data
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _connection.Open();
            try
            {
                using (MySqlCommand command = new MySqlCommand("DELETE FROM tb_student WHERE studentID = @studentID", _connection) )
                {
                    command.Parameters.AddWithValue("@studentID", _selectedStudent.StudentID);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Student data deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting student data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _connection.Close();
            }
        }


        private void find_btn_clicked(object sender, RoutedEventArgs e)
        {
            // Implement find functionality if needed
        }

        private bool ValidateNameLength(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length <= 100;
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
