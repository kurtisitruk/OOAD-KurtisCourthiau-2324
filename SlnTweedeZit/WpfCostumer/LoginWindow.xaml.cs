using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfCostumer
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ActiBuddyDB;Trusted_Connection=True;";
        public LoginWindow()
        {
            InitializeComponent();
            LoginBtn.Click += LoginBtn_Click;
        }



        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTbx.Text;
            string password = PasswordBox.Password;

            // Check if the user is an admin
            if (ValidateUser(username, password, out bool isAdmin))
            {

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();

            }
            else
            {
                ErrorMessageTbc.Text = "Invalid username or password.";
            }
        }



        private bool ValidateUser(string username, string password, out bool isAdmin)
        {
            string query = "SELECT IsAdmin FROM Persoon WHERE login = @username AND paswoord = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        isAdmin = Convert.ToBoolean(result);
                        return true;
                    }
                    else
                    {
                        isAdmin = false;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    isAdmin = false;
                    return false;
                }
            }
        }
    }
}
