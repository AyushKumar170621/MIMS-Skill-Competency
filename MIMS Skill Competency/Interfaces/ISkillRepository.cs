using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Interfaces
{
    public interface ISkillRepository
    {
        public ICollection<SkillDomain> getSkillDomains();

        public ICollection<Skill> getSkillBySkillDomain(List<SkillDomain> skillDomains);    
        IEnumerable<string> GetSkillDomainType();
        IEnumerable<string> GetSkillLevel();
    }
}
