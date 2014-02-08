using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefenseTraining.Model
{
    public class NewAlotmentStatement
    {
        public string EventType { get; set; }
        public string AlotmentType { get; set; }
        public int Availed { get; set; }
        public int Regretted { get; set; }
        public int Total { get { return Availed + Regretted; } }
    }
}
