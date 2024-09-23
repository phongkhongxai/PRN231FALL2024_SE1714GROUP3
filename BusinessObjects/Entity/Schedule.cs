namespace BusinessObjects.Entity
{
    public class Schedule
    {
        public long Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Location { get; set; }
        public long? UserId { get; set; }

    }
}
