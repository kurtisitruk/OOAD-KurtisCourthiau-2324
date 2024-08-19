using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLActiBuddy
{
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
        private static readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ActiBuddyDB;Trusted_Connection=True;";

        public static void UpdatePersoon(Persoon persoon)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Persoon 
                                 SET Voornaam = @Voornaam, 
                                     Achternaam = @Achternaam, 
                                     Login = @Login, 
                                     Paswoord = @Paswoord, 
                                     Profielfoto = @Profielfoto, 
                                     Regdatum = @Regdatum, 
                                     IsAdmin = @IsAdmin 
                                 WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", persoon.Id);
                    cmd.Parameters.AddWithValue("@Voornaam", persoon.Voornaam);
                    cmd.Parameters.AddWithValue("@Achternaam", persoon.Achternaam);
                    cmd.Parameters.AddWithValue("@Login", persoon.Login);
                    cmd.Parameters.AddWithValue("@Paswoord", persoon.Paswoord);
                    cmd.Parameters.AddWithValue("@Profielfoto", persoon.Profielfoto ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Regdatum", persoon.Regdatum);
                    cmd.Parameters.AddWithValue("@IsAdmin", persoon.IsAdmin);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}