namespace BusinessObjects.Entity
{
    public class Application
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public long UserId { get; set; }
        public string? Status { get; set; }
        public string? Resume { get; set; }

        public Job Job { get; set; }
        public User User { get; set; }
        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    }
}
