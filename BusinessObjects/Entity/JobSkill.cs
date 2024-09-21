namespace BusinessObjects.Entity
{
    public class JobSkill
    {
        public long JobId { get; set; }
        public long SkillId { get; set; }
        public string? Experiences { get; set; }

        public Job Job { get; set; }
        public Skill Skill { get; set; }
    }
}
