using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Interfaces
{
    public interface ISkillRepository
    {
        public ICollection<SkillDomain> getSkillDomains();

        public ICollection<Skill> getSkillBySkillDomain(List<SkillDomain> skillDomains);    
        IEnumerable<string> GetSkillDomainType();
        IEnumerable<string> GetSkillLevel();


        ICollection<SearchEmpSkill> SearchEmployees(
         //int managerId,
            List<string> employeeNames = null,
            List<string> skillDomainTypes = null,
            List<string> skillDomains = null,
            List<string> skills = null,
            List<string> skillLevels = null,
            string experienceYears = null,
            string experienceMonth = null);
    }
}
