using CLActiBuddy;
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
    /// Interaction logic for ActiviteitDetailsPage.xaml
    /// </summary>
    public partial class ActiviteitDetailsPage : Page
    {
        public ActiviteitDetailsPage(Activiteit activiteit)
        {
            InitializeComponent();

            ActivityName.Text = activiteit.Naam;
            ActivityDate.Text = activiteit.Datum.ToString("dd-MM-yyyy");
            ActivityDescription.Text = activiteit.Beschrijving;
            ActivityOrganisator.Text = $"Organisator: {activiteit.Organisator}";
            ActivityMaxPersonen.Text = $"Max Personen: {activiteit.MaxPersonen}";
            ActivityIngeschreven.Text = $"Ingeschreven: {activiteit.DeelnameLijst.Count}";

            // Assuming you have a default image or a placeholder image
            ActivityImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/default.png"));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
