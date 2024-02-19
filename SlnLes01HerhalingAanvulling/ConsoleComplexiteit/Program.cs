using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleComplexiteit
{
    class Program
    {
        static void Main(string[] args)
        {
            string woord = " ";
            do
            {
                Console.Write("Geef een woord (enter om te stoppen): ");
                woord = Console.ReadLine();

                if (woord == "")
                {
                    Console.WriteLine("Bedankt en tot ziens");
                    Console.ReadLine();
                }

                else if (IsLetter(woord) == false) 
                {
                    Console.WriteLine("Gelieve een woord te schrijven met alleen letters");
                }

                else
                {
                    Console.WriteLine("aantal karakters: " + AantalKarakters(woord));
                    Console.WriteLine("aantal lettergrepen: " + AantalLettergrepen(woord));
                    Console.WriteLine("complexiteit: " + Complexiteit(woord));
                }
            } while (woord != "");

            Console.ReadLine();
        }

        static int AantalKarakters(string woord)
        {
            int karakters=0;
            foreach (char letter in woord) 
            {
                karakters++;
            }

            return karakters;
        }

        static int AantalLettergrepen(string woord)
        {
            int AantalLettergrepen = 0;
            int index = 0;
            string woord2 = "w" + woord;
            foreach (char letter1 in woord)
            {

                char letter2 = woord2[index];
                if (IsKlinker(letter1) == true && IsKlinker(letter2) == false)
                {
                    AantalLettergrepen++;
                }
                index++;
            }

            return AantalLettergrepen;
        }

        static bool IsKlinker(char letter)
        {
            bool Klinker = false;
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

        static int Complexiteit(string woord)
        {
            int lettersxyq = 0;
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
                        lettersxyq++;break;
                    default: break;
                }
            }
            int complexiteit = ((AantalKarakters(woord) / 3) + AantalLettergrepen(woord) + lettersxyq);

                return complexiteit;
        }

        static bool IsLetter (string woord)
        {
            bool IsHetEenLetter = true;
                foreach (char karakter in woord)
                {

            
                    if (!((Convert.ToInt32(karakter)<91) && (Convert.ToInt32(karakter)>64))  || !((Convert.ToInt32(karakter)<126) && (Convert.ToInt32(karakter)>96) ))
                    {
                        IsHetEenLetter = false;
                    }
                }
            return IsHetEenLetter;
        }
    }
}
