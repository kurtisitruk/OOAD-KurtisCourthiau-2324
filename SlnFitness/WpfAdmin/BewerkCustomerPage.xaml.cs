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
using System.Configuration;
using System.Data.SqlClient;

namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for BewerkCustomerPage.xaml
    /// </summary>
    public partial class BewerkCustomerPage : Page
    {
        private Person _person;

        public BewerkCustomerPage(Person person)
        {
            InitializeComponent();
            _person = person;
            DataContext = new PersonViewModel
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Login = person.Login,
                IsAdmin = person.IsAdmin
            };
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (PersonViewModel)DataContext;
            _person.FirstName = viewModel.FirstName;
            _person.LastName = viewModel.LastName;
            _person.Login = viewModel.Login;
            _person.IsAdmin = viewModel.IsAdmin;

            if (!string.IsNullOrWhiteSpace(viewModel.Password))
            {
                // Hash the password before saving
                _person.Password = HashPassword(viewModel.Password);
            }

            // Save _person to the database (implement this method in your data access layer)
            SavePersonToDatabase(_person);

            // Navigate back to the list page or show a success message
            MessageBox.Show("Customer details updated successfully.");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the previous page
            NavigationService.GoBack();
        }

        private string HashPassword(string password)
        {
            // Implement password hashing here
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private void SavePersonToDatabase(Person person)
        {
            // Implement the logic to save the person to the database
            using (var context = new FitnessDbContext())
            {
                context.Persons.Update(person);
                context.SaveChanges();
            }
        }
    }
}
