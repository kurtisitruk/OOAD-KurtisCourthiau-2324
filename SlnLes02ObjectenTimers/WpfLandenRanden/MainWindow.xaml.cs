using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace WpfLandenRanden
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private Stopwatch stopwatch;
        private List<string> countries;
        private List<TimeSpan> responseTimes;
        private int correctCount;
        private int tickCount;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Voeg landen toe
            countries = new List<string> { "France", "Germany", "Italy", "Spain", "UK" };

            // Schud de landenlijst door
            ShuffleCountries();

            // Initialiseer timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100); // Interval van 100ms
            timer.Tick += Timer_Tick;

            // Initialiseer stopwatch
            stopwatch = new Stopwatch();

            // Initialiseer lijst met antwoordtijden
            responseTimes = new List<TimeSpan>();

            // Start de timer
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tickCount++;
            if (tickCount % 30 == 0)
            {
                ShowNextCountry();
            }
        }

        private void ShowNextCountry()
        {
            if (countries.Any())
            {
                stopwatch.Restart();
                string country = countries.First();
                countries.RemoveAt(0);
                MessageBox.Show(country);
            }
            else
            {
                timer.Stop();
                DisplayResults();
            }
        }

        private void Image_MouseUp(object sender, RoutedEventArgs e)
        {
            Image clickedImage = sender as Image;
            string guessedCountry = clickedImage.Tag.ToString();
            TimeSpan responseTime = stopwatch.Elapsed;

            if (guessedCountry == countries.First())
            {
                correctCount++;
                responseTimes.Add(responseTime);
                resultLabel.Content = "Juist!";
                PlayCorrectSound();
            }
            else
            {
                responseTimes.Add(TimeSpan.Zero);
                resultLabel.Content = "Fout!";
                PlayIncorrectSound();
            }
        }

        private void DisplayResults()
        {
            double averageTime = responseTimes.Where(time => time != TimeSpan.Zero).Average(time => time.TotalSeconds);
            MessageBox.Show($"Aantal juiste antwoorden: {correctCount}\nGemiddelde antwoordtijd: {averageTime:F2} seconden");
        }

        private void ShuffleCountries()
        {
            Random rng = new Random();
            int n = countries.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = countries[k];
                countries[k] = countries[n];
                countries[n] = value;
            }
        }

        private void PlayCorrectSound()
        {
            
        }

        private void PlayIncorrectSound()
        {
            
        }
    }
}
