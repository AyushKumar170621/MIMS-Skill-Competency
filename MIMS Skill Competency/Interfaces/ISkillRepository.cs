using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Interfaces
{
    public interface ISkillRepository
    {
        //ICollection<Employee> getMangersEmployee(int managerId);
<<<<<<< HEAD
        IEnumerable<string> GetDistinctSkillDomainType(int employeeId);
        public ICollection<SkillDomain> getSkillDomains();

        public ICollection<Skill> getSkillBySkillDomain(List<SkillDomain> skillDomains);    
=======
        IEnumerable<string> GetSkillDomainType();
        IEnumerable<string> GetSkillLevel();
        ICollection<EmployeeSkill> getEmployeeSkills(SearchEmpSkill skl);
>>>>>>> ff7a7fc87cc3646556f4413418ca13ca62cfd1fb
    }
}
