namespace MIMS_Skill_Competency.Models
{
    public class SearchEmpSkill
    {
        public List<int> EmployeeIds { get; set; } = new List<int>();
        public List<int> SkillDomainTypes { get; set; } = new List<int>();
        public List<int> SkillDomains { get; set; } = new List<int>();
        public List<int> Skills { get; set; } = new List<int>();
        public List<int> SkillLevels { get; set; } = new List<int>();
        public int? MinExperience { get; set; }
        public int? MaxExperience { get; set; }
    }
}
