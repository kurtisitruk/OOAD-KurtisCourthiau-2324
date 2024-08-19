using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMap.NET;
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
using CLActiBuddy;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfCostumer
{
    /// <summary>
    /// Interaction logic for ActiviteitenPage.xaml
    /// </summary>
    public partial class ActiviteitenPage : Page
    {

        public ActiviteitenPage()
        {
            InitializeComponent();
            LoadActivities();
        }

        private void LoadActivities()
        {
            try
            {
                // Fetch activities from the database
                List<Activiteit> activiteiten = Activiteit.GetActivities();

                // Bind the activities to the DataGrid
                ActivitiesDataGrid.ItemsSource = activiteiten;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading activities: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Refresh the activities list when the refresh button is clicked
            LoadActivities();
        }
    }
}
