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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace Project
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private const string ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=FitnessDB;Integrated Security=True";
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTbx.Text;
            string password = PasswordBox.Password;

            // Perform authentication logic (replace with your actual authentication logic)
            if (AuthenticateUser(username, password))
            {
                // If authentication successful, close the login window
                DialogResult = true;
            }
            else
            {
                // Show error message if authentication fails
                ErrorMessageTbc.Text = "Invalid username or password.";
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            // Replace with your actual authentication logic (e.g., database query, API call)
            // Example logic (for demonstration purposes only):
            return username == "admin" && password == "password";
        }
    }
}
