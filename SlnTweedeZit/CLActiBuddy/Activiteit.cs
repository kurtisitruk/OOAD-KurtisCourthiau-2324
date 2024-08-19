using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLActiBuddy
{
    public class Activiteit
    {
        private static readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ActiBuddyDB;Trusted_Connection=True;";

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public DateTime Datum { get; set; }
        public string Icoon { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int MaxPersonen { get; set; }
        public ActiviteitSoort Soort { get; set; }
        public int Leeftijdsgroep { get; set; }
        public int Moeilijkheid { get; set; }
        public int Niveau { get; set; }
        public int Sector { get; set; }
        public int Organisator { get; set; }

        // Navigation property
        public virtual ICollection<Deelname> DeelnameLijst { get; set; }

        // Constructor
        public Activiteit()
        {
            DeelnameLijst = new List<Deelname>();
        }

        public static List<Activiteit> GetActivities()
        {
            var activiteitenList = new List<Activiteit>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Naam, Beschrijving, Datum, Icoon, Longitude, Latitude, MaxPersonen, Soort, Leeftijdsgroep, Moeilijkheid, Niveau, Sector, Organisator FROM Activiteiten";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var activiteit = new Activiteit
                        {
                            Id = reader.GetInt32(0),
                            Naam = reader.GetString(1),
                            Beschrijving = reader.GetString(2),
                            Datum = reader.GetDateTime(3),
                            Icoon = reader.GetString(4),
                            Longitude = reader.GetDouble(5),
                            Latitude = reader.GetDouble(6),
                            MaxPersonen = reader.GetInt32(7),
                            Soort = (ActiviteitSoort)reader.GetInt32(8), 
                            Leeftijdsgroep = reader.GetInt32(9),
                            Moeilijkheid = reader.GetInt32(10),
                            Niveau = reader.GetInt32(11),
                            Sector = reader.GetInt32(12),
                            Organisator = reader.GetInt32(13)
                        };

                        activiteitenList.Add(activiteit);
                    }
                }
            }

            return activiteitenList;
        }


    }
}
