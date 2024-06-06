﻿using System.Text;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddStudentPage());
        }

        private void SearchStudent_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SearchStudentPage());
        }
    }
}