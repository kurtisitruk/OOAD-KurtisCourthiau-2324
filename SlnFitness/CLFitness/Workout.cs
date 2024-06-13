using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLFitness
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ExerciseId { get; set; }
        public DateTime Date { get; set; }
        public double? Distance { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }

        public Workout() { }

        public Workout(int personId, int exerciseId, DateTime date, double? distance = null)
        {
            PersonId = personId;
            ExerciseId = exerciseId;
            Date = date;
            Distance = distance;
        }
    }
}
