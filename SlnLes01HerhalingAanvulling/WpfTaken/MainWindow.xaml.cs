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

namespace WpfTaken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }
        private void toekijkenBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckForm();
        }

        private void TerugzettenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TakenLbx.SelectedIndex != -1)
            {
                ListBoxItem item = (ListBoxItem)TakenLbx.SelectedItem;
                TakenLbx.Items.Remove(item);
            }
            CheckButtonsEnabled();
        }

        private void VerwijderenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TakenLbx.SelectedIndex != -1)
            {
                ListBoxItem item = (ListBoxItem)TakenLbx.SelectedItem;
                TakenLbx.Items.Remove(item);
                ControleerLbl.Content = "";
            }
            CheckButtonsEnabled();
        }

        private void CheckForm()
        {
            if (Taaktbx.Text == "")
            {
                ControleerLbl.Content = "Gelieve een naam te geven aan uw taak";
            }
            else if (ProriteitCbx.SelectedIndex == 0)
            {
                ControleerLbl.Content = "Gelieve een prioriteit te selecteren";
            }
            else if (DateDp.SelectedDate == null)
            {
                ControleerLbl.Content = "Gelieve een deadline te selecteren";
            }
            else if (!(AdamRbn.IsChecked == true || BilalRbn.IsChecked == true || ChelseyRbn.IsChecked == true))
            {
                ControleerLbl.Content = "Gelieve een persoon toe te voegen";
            }
            else
            {
                AddTask();
            }
        }

        private void AddTask()
        {
            ListBoxItem item = new ListBoxItem();
            item.Content = Taaktbx.Text;
            item.Background = GetPriorityColor(ProriteitCbx.Text);
            TakenLbx.Items.Add(item);
            Taaktbx.Text = "";
            ProriteitCbx.SelectedIndex = 0;
            DateDp.SelectedDate = null;
            AdamRbn.IsChecked = false;
            BilalRbn.IsChecked = false;
            ChelseyRbn.IsChecked = false;
            ControleerLbl.Content = "";
            CheckButtonsEnabled();
        }

        private void CheckButtonsEnabled()
        {
            VerwijderenBtn.IsEnabled = TakenLbx.SelectedIndex != -1;
            TerugzettenBtn.IsEnabled = TakenLbx.Items.Count > 0;
        }

        private Brush GetPriorityColor(string priority)
        {
            switch (priority.ToLower())
            {
                case "zeer hoog":
                    return Brushes.Red;
                case "hoog":
                    return Brushes.Orange;
                case "matig":
                    return Brushes.Yellow;
                case "laag":
                    return Brushes.Green;
                case "zeer laag":
                    return Brushes.Blue;
                default:
                    return Brushes.Transparent;
            }
        }

        private void TakenLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButtonsEnabled();
        }
    }


}

