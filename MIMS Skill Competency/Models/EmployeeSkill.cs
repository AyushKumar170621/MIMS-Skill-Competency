
namespace MIMS_Skill_Competency.Models
{
    public class EmployeeSkill
    {
        public int SkillEmployeeId { get; set; }
        public int EmployeeId { get; set; }
        public int LevelId {  get; set; }
        public string LevelName { get; set; }
        public int SkillDomainTypeId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public int DomainId { get; set; }
        public string DomainName { get; set; }
       
        public string Comments { get; set; }
        // Navigation property for related comments
       // public ICollection<Comment> Comments { get; set; }
    }
}
