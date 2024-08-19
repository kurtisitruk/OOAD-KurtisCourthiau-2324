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
using System.Data.SqlClient;
using CLActiBuddy;


namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for BewerkCustomerPage.xaml
    /// </summary>
    public partial class BewerkCustomerPage : Page
    {
        private Persoon _selectedPersoon;
        private Persoon currentPersoon;
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ActiBuddyDB;Trusted_Connection=True;";


        public BewerkCustomerPage(Persoon persoon)
        {
            InitializeComponent();

            if (persoon == null)
            {
                // Voor een nieuwe persoon
                currentPersoon = new Persoon();
            }
            else
            {
                // Voor he bewerken van een persoon
                currentPersoon = persoon;
                FillFieldsWithData();
            }

            this.DataContext = currentPersoon; this.DataContext = currentPersoon;
            _selectedPersoon = persoon;
            this.DataContext = _selectedPersoon; 
        }

        private void Annuleren_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void UpdatePersoon(Persoon persoon)
        {
            string query = "UPDATE Persoon SET Voornaam = @Voornaam, Achternaam = @Achternaam, Login = @Login, Paswoord = @Paswoord, IsAdmin = @IsAdmin WHERE id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@Voornaam", persoon.Voornaam);
                comm.Parameters.AddWithValue("@Achternaam", persoon.Achternaam);
                comm.Parameters.AddWithValue("@Login", persoon.Login);
                if (!string.IsNullOrWhiteSpace(persoon.Paswoord))
                {
                    comm.Parameters.AddWithValue("@Paswoord", persoon.Paswoord);
                }
                comm.Parameters.AddWithValue("@IsAdmin", persoon.IsAdmin);
                comm.Parameters.AddWithValue("@Id", persoon.Id);

                conn.Open();
                comm.ExecuteNonQuery();
            }
        }

        private void FillFieldsWithData()
        {
            VoornaamTbx.Text = currentPersoon.Voornaam;
            AchternaamTbx.Text = currentPersoon.Achternaam;
            LoginTbx.Text = currentPersoon.Login;
            IsAdminCbx.IsChecked = currentPersoon.IsAdmin;
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            if (VoornaamTbx.Text == null ||
                AchternaamTbx.Text == null ||
                LoginTbx.Text == null ||
                PaswoorTbx == null) 
            {
                MessageBox.Show("Alle velden moeten worden ingevuld.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (currentPersoon.Id == 0)
            {
                //
                AddNewPersoon(currentPersoon);
            }
            else
            {
                // 
                UpdatePersoon(currentPersoon);
            }

            
            NavigationService.GoBack();
        }

        private void AddNewPersoon(Persoon persoon)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ActiBuddyDB;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("INSERT INTO Persoon (Voornaam, Achternaam, Login, IsAdmin) VALUES (@Voornaam, @Achternaam, @Login, @IsAdmin)", conn);
                    comm.Parameters.AddWithValue("@Voornaam", persoon.Voornaam);
                    comm.Parameters.AddWithValue("@Achternaam", persoon.Achternaam);
                    comm.Parameters.AddWithValue("@Login", persoon.Login);
                    comm.Parameters.AddWithValue("@IsAdmin", persoon.IsAdmin);

                    comm.ExecuteNonQuery();
                    MessageBox.Show("Klant succesvol toegevoegd.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Er is een fout opgetreden bij het toevoegen: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


    }
}