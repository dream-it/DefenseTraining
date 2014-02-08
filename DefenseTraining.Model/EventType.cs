namespace DefenseTraining.Model
{
    public class EventType : ModelBase
    {
        public string Name { get; set; }
        public EventCategory Category { get; set; }
        public string CategoryName { get { return Category.ToString(); } }
    }
}
