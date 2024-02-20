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
        bool IsHetEenLetter; 
        public MainWindow()
        {
            
            InitializeComponent();
            

        }

        private void Analyseerbtn_Click(object sender, RoutedEventArgs e)
        {
            woord = woordtbx.Text;
            AantalKarakters(woord);
            int complexiteitnr = Complexiteit(woord);
            complexiteitlbl.Content = "aantal karakters: " + AantalKarakters(woord) + " aantal lettergrepen: " + AantalLettergrepen(woord) + " Complexiteit: " + complexiteitn;

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
                        return true; 
                    default: 
                        return false;

                }
            
        }

        public bool IsLetter(string woord)
        {
            foreach (char karakter in woord)
            {
                if (IsHetEenLetter == true)
                {
                    if (((karakter <= 'Z') && (karakter >= 'A')) || ((karakter >= 'a') && (karakter <= 'z')))
                    {

                        IsHetEenLetter = true;
                    }
                    else
                    {
                        IsHetEenLetter = false;
                    }
                }
                
            }

            return IsHetEenLetter;
        }

        public int AantalKarakters(string woord)
        {
            return woord.Length;
        }

        public int AantalLettergrepen(string woord)
        {
            int aantalLettergrepen = 0;
            bool vorigeKlinker = false;

            foreach (char letter in woord)
            {
                if (IsKlinker(letter) && !vorigeKlinker)
                {
                    aantalLettergrepen++;
                }
                vorigeKlinker = IsKlinker(letter);
            }

            return aantalLettergrepen;
        }

        public int letterxyz(string woord)
        {
            int ErIsxyq = 0;
            bool xyq = false;
            foreach (char letter in woord)
            {
                switch (Convert.ToString(letter))
                {
                    case "x":
                    case "y":
                    case "q":
                    case "X":
                    case "Y":
                    case "Q":
                        xyq = true; break;
                    default: xyq = false; break;
                }
            }
            if (xyq == true)
            {
                ErIsxyq = 1;
            }
            return ErIsxyq;
        }

        public int Complexiteit(string woord)
        {
                int complexiteit = (AantalKarakters(woord) / 3) + (AantalLettergrepen(woord)) + letterxyz(woord);
                return complexiteit;
        }

       

    }
}
