namespace MIMS_Skill_Competency.Models
{
    public class SkillLevel
    {
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
