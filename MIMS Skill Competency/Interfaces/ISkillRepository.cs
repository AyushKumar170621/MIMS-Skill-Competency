using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Interfaces
{
    public interface ISkillRepository
    {
        ICollection<Employee> getMangersEmployee(int managerId);
        ICollection<EmployeeSkill> getEmployeeSkills(SearchEmpSkill skl);
    }
}
