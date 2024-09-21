namespace BusinessObjects.Entity
{
    public class Skill
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
        public ICollection<UserSkill> UserSkills { get; set; } = new HashSet<UserSkill>();
    }
}
