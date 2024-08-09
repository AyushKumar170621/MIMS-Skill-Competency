namespace MIMS_Skill_Competency.Models
{
    public class Comment
    {
        public int CommentId { get; set; } // Primary key for the Comment model
        public string Description { get; set; } // Detailed description of the comment
        public DateTime Time { get; set; } // Time of the comment
        public int SkillId { get; set; } // Foreign key referencing EmployeeSkill

        // Navigation property to access the EmployeeSkill if needed
        public EmployeeSkill EmployeeSkill { get; set; }
    }
}
