using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;

namespace IskoRecords
{
    public partial class ViewStudentPage : Page
    {
        private readonly string connectionString = "server=localhost;user=root;password=P@ssw0rd2023!;database=db_iskorecords;port=3306";
        private readonly MySqlConnection mySqlConnection;

        public ViewStudentPage()
        {
            InitializeComponent();
            mySqlConnection = new MySqlConnection(connectionString);
            DisplayIskoRecords();
        }

        private void DisplayIskoRecords()
        {
            var students = new List<Student>();
            try
            {
                mySqlConnection.Open();
                string query = "SELECT * FROM tb_student";
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentID = reader["studentID"].ToString(),
                                FirstName = reader["firstName"].ToString(),
                                MiddleName = reader["middleName"].ToString(),
                                LastName = reader["lastName"].ToString(),
                                YearLevel = int.Parse(reader["yearLevel"].ToString()),
                                Section = reader["section"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                mySqlConnection.Close();
            }

            student_table1.ItemsSource = students;
        }

        public class Student
        {
            public string? StudentID { get; set; }
            public string? FirstName { get; set; }
            public string? MiddleName { get; set; }
            public string? LastName { get; set; }
            public int YearLevel { get; set; }
            public string? Section { get; set; }
        }

        private void RefreshStudents()
        {
            DisplayIskoRecords(); // Refresh the data by reloading from the database
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshStudents();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (student_table1.SelectedItem is Student selectedStudent)
            {
                try
                {
                    mySqlConnection.Open();

                    EditandDeletePage editanddeletepage = new EditandDeletePage(selectedStudent, mySqlConnection);
                    NavigationService.Navigated += NavigationService_selected;
                    NavigationService.Navigate(editanddeletepage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while editing the student: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a student to edit.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void NavigationService_selected(object sender, NavigationEventArgs e)
        {
            mySqlConnection.Close();

            if (NavigationService != null)
            {
                NavigationService.Navigated -= NavigationService_selected;
            }
        }

        private void student_table1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Handle cell edit ending if needed
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change if needed
        }
    }
}
