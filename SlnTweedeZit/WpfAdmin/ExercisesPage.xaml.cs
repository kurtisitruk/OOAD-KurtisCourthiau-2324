using CLActiBuddy;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for ExercisesPage.xaml
    /// </summary>
    public partial class ExercisesPage : Page
    {
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ActiBuddyDB;Trusted_Connection=True;";
        private List<Activiteit> activiteiten = new List<Activiteit>();

        public void ActiviteitenOverzichtPage()
        {
            InitializeComponent();
            LoadActivities();
        }

        private void LoadActivities()
        {
            activiteiten.Clear();
            ActivitiesItemsControl.ItemsSource = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Activiteiten ORDER BY Datum DESC", conn);
                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    var activiteit = new Activiteit
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Naam = Convert.ToString(reader["Naam"]),
                        Beschrijving = Convert.ToString(reader["Beschrijving"]),
                        Datum = Convert.ToDateTime(reader["Datum"]),
                        Icoon = Convert.ToString(reader["Icoon"]),
                        MaxPersonen = Convert.ToInt32(reader["MaxPersonen"]),
                        Organisator = Convert.ToInt32(reader["Organisator"])
                    };

                    activiteiten.Add(activiteit);
                }

                ActivitiesItemsControl.ItemsSource = activiteiten;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterActivities();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterActivities();
        }

        private void TypeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            FilterActivities();
        }

        private void TypeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            FilterActivities();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadActivities();
        }

        private void FilterActivities()
        {
            var filteredActivities = activiteiten.AsEnumerable();

            // Ensure that the search text box value is not null
            if (!string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                var searchText = SearchTextBox.Text.ToLower();
                filteredActivities = filteredActivities.Where(a =>
                    a.Naam.ToLower().Contains(searchText) ||
                    a.Beschrijving.ToLower().Contains(searchText));
            }

            // Filter by selected date
            if (DatePicker.SelectedDate.HasValue)
            {
                filteredActivities = filteredActivities.Where(a => a.Datum.Date == DatePicker.SelectedDate.Value.Date);
            }

            // Apply additional filtering based on type checkboxes if necessary
            if (TypeCheckBox1.IsChecked == true)
            {
                // Apply filtering logic for TypeCheckBox1
            }

            if (TypeCheckBox2.IsChecked == true)
            {
                // Apply filtering logic for TypeCheckBox2
            }

            // Update the ItemsControl with the filtered activities
            ActivitiesItemsControl.ItemsSource = filteredActivities.OrderByDescending(a => a.Datum).ToList();
        }

        private void DeleteActiviteit(Activiteit activiteit)
        {
            MessageBoxResult result = MessageBox.Show($"Weet u zeker dat u de activiteit '{activiteit.Naam}' wilt verwijderen?", "Bevestigen", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("DELETE FROM Activiteiten WHERE Id = @Id", conn);
                    comm.Parameters.AddWithValue("@Id", activiteit.Id);
                    comm.ExecuteNonQuery();
                }

                LoadActivities();
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var activiteit = (Activiteit)button.DataContext;
            NavigationService.Navigate(new ActiviteitDetailsPage(activiteit));
        }
    }
}
