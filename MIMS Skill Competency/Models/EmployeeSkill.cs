namespace MIMS_Skill_Competency.Models
{
    public class EmployeeSkill
    {
        public int Id { get; set; }
        public string skillDomainType { get; set; }
        public string skillDomain { get; set; }
        public string skillLevel { get; set; }

        public string skill{ get; set; }
        public int experienceYear { get; set; }
        public int experienceMonth { get; set; }
        public List<string> comments { get; set; }
    }
}
