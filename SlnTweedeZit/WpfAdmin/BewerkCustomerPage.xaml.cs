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

namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for BewerkCustomerPage.xaml
    /// </summary>
    public partial class BewerkCustomerPage : Page
    {
        private Persoon _selectedPersoon;
        private DatabaseHelper _dbHelper;

        public BewerkCustomerPage(Persoon persoon)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();

            // Set the DataContext to the selected Persoon
            _selectedPersoon = persoon;
            this.DataContext = _selectedPersoon;
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            // Save changes to the database
            _dbHelper.UpdatePersoon(_selectedPersoon);
            MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Optionally navigate back to the Customer List Page
            NavigationService.GoBack();
        }

        private void Annuleren_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the Customer List Page without saving
            NavigationService.GoBack();
        }
    }
}
