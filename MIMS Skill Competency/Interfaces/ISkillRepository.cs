using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Interfaces
{
    public interface ISkillRepository
    {
        //ICollection<Employee> getMangersEmployee(int managerId);
        IEnumerable<string> GetDistinctSkillDomainType(int employeeId);
        public ICollection<SkillDomain> getSkillDomains();

        public ICollection<Skill> getSkillBySkillDomain(List<SkillDomain> skillDomains);    
    }
}
