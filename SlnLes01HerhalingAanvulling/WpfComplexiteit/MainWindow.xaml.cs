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

namespace WpfComplexiteit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string woord;
        bool Klinker = false;
        bool IsHetEenLetter = false;
        int karakters = 0;
        int Lettergrepen = 0;
        char letter;
        public MainWindow()
        {
            
            InitializeComponent();
            

    }

        private void Analyseerbtn_Click(object sender, RoutedEventArgs e)
        {
            woord = woordtbx.Text;
            IsKlinker(letter);
            IsLetter(woord);
            AantalKarakters(woord);
            AantalLettergrepen(woord);
            complexiteitlbl.Content = "aantal karakters: " + AantalKarakters(woord) + " aantal lettergrepen: " + AantalLettergrepen(woord);

        }

        public bool IsKlinker(char letter)
        {
            
                switch (Convert.ToString(letter))
                {
                    case "a":
                    case "e":
                    case "i":
                    case "o":
                    case "u":
                    case "y":
                        Klinker = true; break;
                    default: break;
                }
            return Klinker;
            
        }

        public void IsLetter(string woord)
        {
            foreach (char karakter in woord)
            {


                if (((karakter<='Z') && (karakter>='A')) || ((karakter >= 'a') && (karakter <= 'z')))
                {
                    IsHetEenLetter = true;
                    return;
                }
                
            }
        }

        public int AantalKarakters(string woord)
        {
            int karakters = 0;
            foreach (char letter in woord)
            {
                karakters++;
            }
            return karakters;
        }

        public int AantalLettergrepen(string woord)
        {
            int index = 0;
            string woord2 = "w" + woord;
            foreach (char letter1 in woord)
            {

                char letter2 = woord2[index];
                if (IsKlinker(letter1) == false && IsKlinker(letter2) == true)
                {
                    Lettergrepen++;
                }
                index++;
            }

            return Lettergrepen;
        }



    }
}
