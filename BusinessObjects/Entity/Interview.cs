namespace BusinessObjects.Entity
{
    public class Interview
    {
        public long Id { get; set; }
        public long ApplicationId { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public string? Status { get; set; }
        public string? Result { get; set; }
        public long? Round { get; set; }

        public Application Application { get; set; }
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
