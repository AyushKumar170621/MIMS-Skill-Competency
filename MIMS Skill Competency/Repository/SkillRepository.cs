using MIMS_Skill_Competency.Data;
using MIMS_Skill_Competency.Interfaces;
using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public SkillRepository(ApplicationDbContext dbContext) {
            _dbcontext = dbContext;
        }
        public ICollection<EmployeeSkill> getEmployeeSkills(SearchEmpSkill skl)
        {
            throw new NotImplementedException();
        }

        public ICollection<Employee> getMangersEmployee(int managerId)
        {
            throw new NotImplementedException();
        }
    }
}
