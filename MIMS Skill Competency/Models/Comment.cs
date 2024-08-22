namespace MIMS_Skill_Competency.Models
{
    public class Comment
    {

        public int CommentId { get; set; } // Add primary key for Comment
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        // Foreign key
        public int EmployeeSkillId { get; set; }

        // Navigation property
        public EmployeeSkill EmployeeSkill { get; set; }
    }
}
