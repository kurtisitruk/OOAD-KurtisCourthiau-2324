using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLFitness
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Instruction { get; set; }
        public string BodyPart { get; set; }
        public string Pose { get; set; }
        public string Nickname { get; set; }
    }
}
