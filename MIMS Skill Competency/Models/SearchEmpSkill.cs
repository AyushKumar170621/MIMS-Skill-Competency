namespace MIMS_Skill_Competency.Models
{
    public class SearchEmpSkill
    {
        public List<Employee> Employees { get; set; }
        public List<string> DomainType { get; set; }
        public List<string> SkillDomain { get; set; }
        public List<string> Skill { get; set; }

        public List<string> SkillLevel { get; set; }
        public int ExperienceMonth { get; set; }
        public int ExperienceYear { get; set; }
    }
}
