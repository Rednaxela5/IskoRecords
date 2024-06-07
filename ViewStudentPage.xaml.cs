using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IskoRecords
{
    public partial class SearchStudentPage : Page
    {
        private const string filePath = "student_data.txt";

        public SearchStudentPage()
        {
            InitializeComponent();
            LoadDataFromFile();
        }

        private void LoadDataFromFile()
        {
            if (File.Exists(filePath))
            {
                List<string> lines = File.ReadAllLines(filePath).ToList();
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    student_table1.Items.Add(new Student
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
        }

        private void SaveDataToFile()
        {
            List<string> lines = new List<string>();
            foreach (Student student in student_table1.Items)
            {
                string line = $"{student.StudentID},{student.FirstName},{student.MiddleName},{student.LastName},{student.YearLevel},{student.Section}";
                lines.Add(line);
            }
            File.WriteAllLines(filePath, lines);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Handle edit and delete operations
            SaveDataToFile();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection changed event
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
