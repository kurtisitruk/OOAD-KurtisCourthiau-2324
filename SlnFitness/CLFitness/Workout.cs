using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLFitness
{
    public class Workout
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ExerciseId { get; set; }
        public DateTime Date { get; set; }
        public float? Distance { get; set; }
    }
}
