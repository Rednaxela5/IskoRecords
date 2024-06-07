using System;
using System.Collections.Generic;
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
    public partial class AddStudentPage : Page
    {
        public class Student
        {
            public string student_id { get; set; }
            public string first_name { get; set; }
            public string middle_name { get; set; }
            public string last_name { get; set; }
            public string year_level { get; set; }
            public string section { get; set; }
        }

        private List<Student> _students;
        private const string FilePath
        public AddStudentPage()
        {
            InitializeComponent();
        }

        private void student_id_entry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void submit_btn(object sender, RoutedEventArgs e)
        {

        }

        private void clear_btn(object sender, RoutedEventArgs e)
        {

        }
    }
}