namespace DefenseTraining.Model
{
    public class EventFilter
    {
        public EventFilter()
        {
            this.Name = string.Empty;
            this.PersonalNo = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int EventTypeId { get; set; }
        public int GenreId { get; set; }
        public int SpecialityId { get; set; }
        public int CountryId { get; set; }
        public int RankId { get; set; }
        public string PersonalNo { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int StartIndex { get; set; }
        public int DisplayCount { get; set; }
    }
}
