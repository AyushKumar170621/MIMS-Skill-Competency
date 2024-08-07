namespace MIMS_Skill_Competency.Models
{
    public class EmployeeSkill
    {
        public int skill_id { get; set; }
        public string skill_domain_type { get; set; }
        public string skill_domain { get; set; }
        public string skill { get; set; }
        public string skill_level { get; set; }        
        public int experience_month { get; set; }
        public int experience_year { get; set; }
        public string comment { get; set; }
        public int emp_id { get; set; }
    }
}
