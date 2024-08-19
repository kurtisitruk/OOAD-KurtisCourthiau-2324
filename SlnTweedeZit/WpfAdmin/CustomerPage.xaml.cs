using System;
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
using CLActiBuddy;

namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ActiBuddyDB;Trusted_Connection=True;";

        public CustomerPage()
        {
            InitializeComponent();
            LoadCustomers();
        }

        public void LoadCustomers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // open connectie
                conn.Open();

                // voer SQL commando uit
                SqlCommand comm = new SqlCommand("SELECT * FROM Persoon", conn);
                SqlDataReader reader = comm.ExecuteReader();

                // lees en verwerk resultaten
                while (reader.Read())
                {
                    // lees record velden 
                    int id = Convert.ToInt32(reader["id"]);
                    string Voornaam = Convert.ToString(reader["Voornaam"]);
                    string Achternaam = Convert.ToString(reader["Achternaam"]);
                    string Username = Convert.ToString(reader["Login"]);
                    string Password = Convert.ToString(reader["Paswoord"]);
                    DateTime RegDatum = Convert.ToDateTime(reader["RegDatum"]);
                    bool isAdmin = Convert.ToBoolean(reader["IsAdmin"]);

                    if (isAdmin == false)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Content = $"{id} :{Voornaam} {Achternaam} {RegDatum}";
                        item.Tag = id;
                        lbxAllePersonen.Items.Add(item);
                    }

                }
            }
        }

        private Persoon GetPersoonById(int id)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Persoon WHERE id = @id", conn);
                comm.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = comm.ExecuteReader();

                if (reader.Read())
                {
                    Persoon pers = new Persoon
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Voornaam = Convert.ToString(reader["Voornaam"]),
                        Achternaam = Convert.ToString(reader["Achternaam"]),
                        Login = Convert.ToString(reader["Login"]),
                        Paswoord = Convert.ToString(reader["Paswoord"]),
                        Regdatum = Convert.ToDateTime(reader["RegDatum"]),
                        IsAdmin = Convert.ToBoolean(reader["IsAdmin"]),

                    };
                    return pers;
                }
            }
            return null;
        }

        private void bewerkBtn_Click_1(object sender, RoutedEventArgs e)
        {
            // Get the selected ListBoxItem
            ListBoxItem selectedItem = lbxAllePersonen.SelectedItem as ListBoxItem;

            if (selectedItem != null)
            {
                // Retrieve the ID from the Tag property
                int selectedId = (int)selectedItem.Tag;

                // Fetch the corresponding Persoon object based on the ID
                Persoon selectedPersoon = GetPersoonById(selectedId);

                if (selectedPersoon != null)
                {
                    // Navigate to the BewerkCustomerPage with the selected Persoon
                    BewerkCustomerPage bewerkPage = new BewerkCustomerPage(selectedPersoon);
                    NavigationService?.Navigate(bewerkPage);

                }
                else
                {
                    MessageBox.Show("Failed to retrieve the customer details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Select a customer to edit.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void VerwijdeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected ListBoxItem
            ListBoxItem selectedItem = lbxAllePersonen.SelectedItem as ListBoxItem;

            if (selectedItem != null)
            {
                // Retrieve the ID from the Tag property
                int selectedId = (int)selectedItem.Tag;

                // Retrieve the full name for confirmation
                Persoon selectedPersoon = GetPersoonById(selectedId);

                if (selectedPersoon != null)
                {
                    // Show confirmation message
                    MessageBoxResult result = MessageBox.Show($"Weet u zeker dat u {selectedPersoon.Voornaam} {selectedPersoon.Achternaam} wilt verwijderen?", "Bevestigen", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Perform the deletion
                        DeletePersoon(selectedId);

                        // Refresh the customer list after deletion
                        lbxAllePersonen.Items.Clear();
                        LoadCustomers();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecteer een klant om te verwijderen.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeletePersoon(int id)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ActiBuddyDB;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("DELETE FROM Persoon WHERE id = @id", conn);
                    comm.Parameters.AddWithValue("@id", id);

                    int rowsAffected = comm.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Klant succesvol verwijderd.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Verwijderen mislukt. Klant niet gevonden.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Er is een fout opgetreden bij het verwijderen: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void NieuwCBtn_Click(object sender, RoutedEventArgs e)
        {
            BewerkCustomerPage bewerkPage = new BewerkCustomerPage(null); // Passing null indicates adding a new customer
            NavigationService.Navigate(bewerkPage);
        }
    }
}
