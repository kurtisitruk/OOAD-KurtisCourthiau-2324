using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLActiBuddy
{
    public enum ActiviteitSoort
    {
        Sport,
        Cultuur,
        Hobby
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

