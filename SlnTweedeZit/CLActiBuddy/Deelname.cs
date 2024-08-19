using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLActiBuddy
{
    public class Deelname
    {
        public int Id { get; set; }
        public int PersoonId { get; set; }
        public int ActiviteitId { get; set; }

        // Navigation properties
        public virtual Persoon Deelnemer { get; set; }
        public virtual Activiteit Activiteit { get; set; }
    }
}
