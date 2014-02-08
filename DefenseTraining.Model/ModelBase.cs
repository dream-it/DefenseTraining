namespace DefenseTraining.Model
{
    public class ModelBase
    {
        public ModelBase()
        {
            IsActive = true;
        }

        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
