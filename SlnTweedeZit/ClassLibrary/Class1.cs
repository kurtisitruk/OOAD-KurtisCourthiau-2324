using System.Data.SqlClient;


namespace ClassLibrary
{

    public enum ActiviteitSoort
    {
        Sport,
        Cultuur,
        Hobby
    }

    public class Persoon
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Login { get; set; }
        public string Paswoord { get; set; }
        public string Profielfoto { get; set; }
        public DateTime Regdatum { get; set; }
        public bool IsAdmin { get; set; }

        // Navigation property
        public virtual ICollection<Deelname> DeelnameLijst { get; set; }

        // Constructor
        public Persoon()
        {
            DeelnameLijst = new List<Deelname>();
        }
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ActiBuddyDB;Trusted_Connection=True;";

        public void UpdatePersoon(Persoon persoon)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(
                    "UPDATE Persoon SET Voornaam = @Voornaam, Achternaam = @Achternaam, Login = @Login, Paswoord = @Paswoord, IsAdmin = @IsAdmin WHERE Id = @Id", conn);

                comm.Parameters.AddWithValue("@Voornaam", persoon.Voornaam);
                comm.Parameters.AddWithValue("@Achternaam", persoon.Achternaam);
                comm.Parameters.AddWithValue("@Login", persoon.Login);
                comm.Parameters.AddWithValue("@Paswoord", persoon.Paswoord);
                comm.Parameters.AddWithValue("@IsAdmin", persoon.IsAdmin);
                comm.Parameters.AddWithValue("@Id", persoon.Id);

                comm.ExecuteNonQuery();
            }
        }
    }

    public class Activiteit
    {
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
    }

    public class Deelname
    {
        public int Id { get; set; }
        public int PersoonId { get; set; }
        public int ActiviteitId { get; set; }

        // Navigation properties
        public virtual Persoon Deelnemer { get; set; }
        public virtual Activiteit Activiteit { get; set; }
    }

    public class DatabaseHelper
    {
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ActiBuddyDB;Trusted_Connection=True;";

        public List<Persoon> GetPersonen()
        {
            List<Persoon> personen = new List<Persoon>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("SELECT id, voornaam, achternaam, regdatum, isAdmin FROM Persoon", conn);
                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    Persoon pers = new Persoon
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Voornaam = Convert.ToString(reader["voornaam"]),
                        Achternaam = Convert.ToString(reader["achternaam"]),
                        Regdatum = Convert.ToDateTime(reader["regdatum"]),
                        IsAdmin = Convert.ToBoolean(reader["isAdmin"])
                    };

                    personen.Add(pers);
                }
            }

            return personen;
        }
    }

   
}
