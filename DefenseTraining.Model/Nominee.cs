
namespace DefenseTraining.Model
{
    public class Nominee
    {
        public Nominee()
        {
            Rank = new Rank();
            Unit = new Unit();
            Branch = new Branch();
        }

        public string PersonalNo { get; set; }
        public string Name { get; set; }
        public Rank Rank { get; set; }
        public Unit Unit { get; set; }
        public Branch Branch { get; set; }
    }
}
