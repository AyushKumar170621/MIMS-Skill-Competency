using AutoMapper;
using MIMS_Skill_Competency.Dtos;
using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Mappers
{
    public class SkillEmployeeMapper : Profile
    {
        public SkillEmployeeMapper() {
            CreateMap<Skill,SkillDTO>();
            CreateMap<SkillDomain,SkillDomainDTO>();
            CreateMap<SkillLevel,SkillLevelDTO>();
            CreateMap<SkillDomainType,SkillDomainTypeDTO>();
        }
    }
}
