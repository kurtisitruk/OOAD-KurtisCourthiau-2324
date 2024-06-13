using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CLFitness
{
    public enum ExerciseType
    {
        Cardio = 1,
        Dumbell = 2,
        Yoga = 3
    }
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ExerciseType Type { get; set; }
        public string Instruction { get; set; }
        public string BodyPart { get; set; }
        public string Pose { get; set; }
        public string Nickname { get; set; }

        public Exercise() { }

        public Exercise(string name, ExerciseType type, string instruction = null, string bodyPart = null, string pose = null, string nickname = null)
        {
            Name = name;
            Type = type;
            Instruction = instruction;
            BodyPart = bodyPart;
            Pose = pose;
            Nickname = nickname;
        }
    }
}
