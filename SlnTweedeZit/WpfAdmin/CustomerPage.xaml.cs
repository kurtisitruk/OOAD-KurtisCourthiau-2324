using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ActiBuddyDB;Trusted_Connection=True;";

        public CustomerPage()
        {
            InitializeComponent();
            LoadCustomers();
        }

        public void LoadCustomers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // open connectie
                conn.Open();

                // voer SQL commando uit
                SqlCommand comm = new SqlCommand("SELECT * FROM Persoon", conn);
                SqlDataReader reader = comm.ExecuteReader();

                // lees en verwerk resultaten
                while (reader.Read())
                {
                    // lees record velden 
                    int id = Convert.ToInt32(reader["id"]);
                    string Voornaam = Convert.ToString(reader["Voornaam"]);
                    string Achternaam = Convert.ToString(reader["Achternaam"]);
                    DateTime RegDatum = Convert.ToDateTime(reader["RegDatum"]);
                    bool isAdmin = Convert.ToBoolean(reader["IsAdmin"]);

                    if (isAdmin == false)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Content = $"{id} :{Voornaam} {Achternaam} {RegDatum}";
                        item.Tag = id;
                        lbxAllePersonen.Items.Add(item);
                    }

                }

    }
}
    }
}
