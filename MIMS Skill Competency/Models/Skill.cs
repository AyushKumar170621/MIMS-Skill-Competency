namespace MIMS_Skill_Competency.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public int DomainId { get; set; }
        public string SkillName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
