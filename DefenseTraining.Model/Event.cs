using System.Collections.Generic;
using System.Text;

namespace DefenseTraining.Model
{
    public class Event : ModelBase
    {
        public Event()
        {
            Type = new EventType();
            Genre = new Genre();
            Speciality = new Speciality();
            Country = new Country();
            Responsibilities = new List<Responsibility>();
            RequiredDocs = new List<RequiredDoc>();
            InitAlotment = new List<Alotment>();
            ReAlotment = new List<Alotment>();
            Nominees = new List<Nominee>();
            Reminders = new List<Reminder>();
        }

        public string Name { get; set; }
        public EventType Type { get; set; }
        public Genre Genre { get; set; }
        public Speciality Speciality { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public string Institute { get; set; }
        public string Ranks { get; set; }
        public string StartsOn { get; set; }
        public string EndsOn { get; set; }
        public int Vacancies { get; set; }
        public List<Responsibility> Responsibilities { get; set; }
        public List<RequiredDoc> RequiredDocs { get; set; }
        public string AcceptanceOn { get; set; }
        public string NominationOn { get; set; }
        public string DocForwardOn { get; set; }
        public List<Alotment> InitAlotment { get; set; }
        public List<Alotment> ReAlotment { get; set; }
        public List<Nominee> Nominees { get; set; }
        public List<Reminder> Reminders { get; set; }
        public string CommSparatedNominees
        {
            get
            {
                StringBuilder nominees = new StringBuilder();
                foreach (var nominee in this.Nominees)
                {
                    nominees.AppendFormat("{0} {1} {2}, ", nominee.PersonalNo, nominee.Rank.Name, nominee.Name);
                }
                return nominees.ToString().TrimEnd(", ".ToCharArray());
            }
        }

        public List<EventReportForList> ToEventReportForList(ref int slNo)
        {
            List<EventReportForList> reports = new List<EventReportForList>();
            if (this.Nominees.Count == 0)
            {
                EventReportForList erl = new EventReportForList();
                erl.Ser = slNo;
                erl.SvcNo = string.Empty;
                erl.Rank = string.Empty;
                erl.PrticipantName = string.Empty;
                erl.ArmsSvc = this.Genre.Name;
                erl.EventName = this.Name;
                erl.Country = this.Country.Name;
                erl.From = this.StartsOn;
                erl.To = this.EndsOn;
                erl.Vac = this.Vacancies;
                slNo++;
                reports.Add(erl);
                return reports;
            }
            foreach (var item in this.Nominees)
            {
                EventReportForList report = new EventReportForList();
                report.Ser = slNo;
                report.SvcNo = item.PersonalNo;
                report.Rank = item.Rank.Name;
                report.PrticipantName = item.Name;
                report.ArmsSvc = this.Genre.Name;
                report.EventName = this.Name;
                report.Country = this.Country.Name;
                report.Vac = 1;
                report.From = this.StartsOn;
                report.To = this.EndsOn;
                slNo++;
                reports.Add(report);
            }
            return reports;
        }

        public EventReport ToEventReport()
        {
            EventReport er = new EventReport();
            er.Id = this.Id;
            er.Name = this.Name;
            er.Type = this.Type.Name;
            er.Genre = this.Genre.Name;
            er.Speciality = this.Speciality.Name;
            er.Country = this.Country.Name;
            er.City = this.City;
            er.Institute = this.Institute;
            er.Ranks = this.Ranks;
            er.StartsOn = this.StartsOn;
            er.EndsOn = this.EndsOn;
            er.Vacancies = this.Vacancies;
            er.Responsibilities = this.ToResposibilitiesText();
            er.RequiredDocs = this.ToRequiredDocsText();
            er.AcceptanceOn = this.AcceptanceOn;
            er.NominationOn = this.NominationOn;
            er.DocForwardOn = this.DocForwardOn;
            er.InitAlotment = this.ToInitAlotmentText();
            er.ReAlotment = this.ToReAlotmetText();
            er.Nominees = this.ToNomineesText();
            er.CommaSeparatedNominees = this.CommSparatedNominees;
            return er;
        }

        private string ToNomineesText()
        {
            StringBuilder builder = new StringBuilder();
            int i = 1;
            foreach (var item in this.Nominees)
            {
                builder.AppendFormat("\r\n({0})\t{1} {2} {3}", i, item.PersonalNo, item.Rank.Name, item.Name);
                i++;
            }
            return builder.ToString();
        }

        private string ToReAlotmetText()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in this.ReAlotment)
            {
                builder.AppendFormat("{0}: {1}, ", item.Type, item.Quota);
            }
            return builder.ToString().TrimEnd(", ".ToCharArray());
        }

        private string ToInitAlotmentText()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in this.InitAlotment)
            {
                builder.AppendFormat("{0}: {1}, ", item.Type, item.Quota);
            }
            return builder.ToString().TrimEnd(", ".ToCharArray());
        }

        private string ToRequiredDocsText()
        {
            StringBuilder builder = new StringBuilder();
            int i = 1;

            foreach (var item in this.RequiredDocs)
            {
                builder.AppendFormat("\r\n({0})\t{1}", i, item.Name);
                i++;
            }
            return builder.ToString();
        }

        private string ToResposibilitiesText()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in this.Responsibilities)
            {
                builder.AppendFormat("{0}, ", item.Name);
            }
            return builder.ToString().TrimEnd(", ".ToCharArray());
        }
    }
}
