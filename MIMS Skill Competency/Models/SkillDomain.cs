namespace MIMS_Skill_Competency.Models
{
    public class SkillDomain
    {
        public int DomainId { get; set; }
        public string DomainName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int SkillEmployeeId { get; set; }

    }
}
