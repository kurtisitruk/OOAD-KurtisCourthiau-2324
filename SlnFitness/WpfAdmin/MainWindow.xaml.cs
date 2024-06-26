﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using WpfAdmin;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string FitnessDB = ConfigurationManager.ConnectionStrings["FitnessDB"].ConnectionString;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new CustomersPage());
        }

        private void CustomersBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CustomersPage());
        }

        private void ExercisesBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ExercisesPage());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["CurrentUser"] = null;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void TestDatabaseConnection()
        {
            using (var context = new FitnessDbContext())
            {
                var persons = context.Persons.ToList();
                MessageBox.Show($"Found {persons.Count} persons in the database.");
            }
        }

    }
}
