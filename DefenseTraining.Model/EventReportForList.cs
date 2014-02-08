
namespace DefenseTraining.Model
{
    public class EventReportForList
    {
        public EventReportForList()
        {
            SvcNo = string.Empty;
            Rank = string.Empty;
            PrticipantName = string.Empty;
            ArmsSvc = string.Empty;
        }
        public int Ser { get; set; }
        public string SvcNo { get; set; }
        public string Rank { get; set; }
        public string PrticipantName { get; set; }
        public string ArmsSvc { get; set; }
        public string EventName { get; set; }
        public string Country { get; set; }
        public int Vac { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
