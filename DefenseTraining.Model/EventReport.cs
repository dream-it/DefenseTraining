
namespace DefenseTraining.Model
{
    public class EventReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public string Speciality { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Institute { get; set; }
        public string Ranks { get; set; }
        public string StartsOn { get; set; }
        public string EndsOn { get; set; }
        public int Vacancies { get; set; }
        public string Responsibilities { get; set; }
        public string RequiredDocs { get; set; }
        public string AcceptanceOn { get; set; }
        public string NominationOn { get; set; }
        public string DocForwardOn { get; set; }
        public string InitAlotment { get; set; }
        public string ReAlotment { get; set; }
        public string Nominees { get; set; }
        public string CommaSeparatedNominees { get; set; }
    }
}
