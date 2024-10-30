using System.Text.Json.Serialization;

namespace BusinessObjects.Entity
{
    public class UserSkill
    {
        public long UserId { get; set; }
        public long SkillId { get; set; }
        public string? Experiences { get; set; }
        public User User { get; set; }
        public Skill Skill { get; set; }
    }
}
