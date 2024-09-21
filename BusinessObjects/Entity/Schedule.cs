namespace BusinessObjects.Entity
{
    public class Schedule
    {
        public long Id { get; set; }
        public long InterviewId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Location { get; set; }
        public long? UserId { get; set; }

        public User User { get; set; }
        public Interview Interview { get; set; }
    }
}
