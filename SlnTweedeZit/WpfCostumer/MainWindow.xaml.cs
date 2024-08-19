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

namespace WpfCostumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Load initial page
            MainFrame.Content = new ActiviteitenPage();
        }

        // Activiteiten Button Click Event
        private void ActiviteitenBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ActiviteitenPage();
        }

        // Organiseer Button Click Event
        private void OrganiseerBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new OrganiseerPage();
        }

        // Uitloggen Button Click Event
        private void UitloggenBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
