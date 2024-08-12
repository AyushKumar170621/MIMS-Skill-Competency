using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Interfaces
{
    public interface ISkillRepository
    {
        public ICollection<SkillDomain> getSkillDomains();

        public ICollection<Skill> getSkillBySkillDomain(List<SkillDomain> skillDomains);    
        IEnumerable<SkillDomainType> GetSkillDomainType();
        IEnumerable<SkillLevel> GetSkillLevel();


        ICollection<EmployeeSkill> SearchEmployees(
        List<int> employeeIds ,
        List<int> skillDomainTypes,
        List<int> skillDomains ,
        List<int> skills,
        List<int> skillLevels,
        int? experienceYears = null, // Nullable int
        int? experienceMonth = null); // Nullable int
    }
}
