using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CLFitness
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        public Person() { }

        public Person(string username, string passwordHash, bool isAdmin)
        {
            Username = username;
            PasswordHash = passwordHash;
            IsAdmin = isAdmin;
        }
    }

}
