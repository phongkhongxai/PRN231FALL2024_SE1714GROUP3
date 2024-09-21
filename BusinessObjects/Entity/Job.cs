namespace BusinessObjects.Entity
{
    public class Job
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Position { get; set; }
        public long Amount { get; set; }

        public User User { get; set; }
        public ICollection<Application> Applications { get; set; } = new List<Application>();
        public ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
    }
}
