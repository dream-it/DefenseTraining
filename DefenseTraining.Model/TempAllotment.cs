using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefenseTraining.Model
{
    public class TempAllotment
    {
        public int EventId { get; set; }
        public string EventTypeName { get; set; }
        public AlotmentType AllotmentType { get; set; }
        public EventCategory EventCategory { get; set; }
        public int Quota { get; set; }
    }
}
