namespace MIMS_Skill_Competency.Models
{
    public class SkillDomainType
    {
        public int SkillDomainTypeId { get; set; }
        public string DomainType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
