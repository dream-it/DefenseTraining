using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefenseTraining.Model
{
    public class JointStatement
    {
        public JointStatement()
        {
        }

        public string Service { get; set; }
        public int CourseAvailed { get; set; }
        public int CourseRegretted { get; set; }
        public int SeminarAvailed { get; set; }
        public int SeminarRegretted { get; set; }
        public int SmeeAvailed { get; set; }
        public int SmeeRegretted { get; set; }
        public int VisitAvailed { get; set; }
        public int VisitRegretted { get; set; }
        public int CompetitionAvailed { get; set; }
        public int CompetitionRegretted { get; set; }
        public int ExcerciseAvailed { get; set; }
        public int ExcerciseRegretted { get; set; }
        public int TotlaAvailed { get { return (CourseAvailed + SeminarAvailed + SmeeAvailed + VisitAvailed + CompetitionAvailed + ExcerciseAvailed); } }
        public int TotalRegretted { get { return (CourseRegretted + SeminarRegretted + SmeeRegretted + VisitRegretted + CompetitionRegretted + ExcerciseRegretted); } }
    }
}
