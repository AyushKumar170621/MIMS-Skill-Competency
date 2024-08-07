using Dapper;
using MIMS_Skill_Competency.Data;
using MIMS_Skill_Competency.Interfaces;
using MIMS_Skill_Competency.Models;
using System.Data;

namespace MIMS_Skill_Competency.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public SkillRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public IEnumerable<string> GetSkillDomainType()
        {
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                List<string> skillDomainType = new List<string>{ "Core", "Secondary"};

                return skillDomainType;
            }
        }

        public IEnumerable<string> GetSkillLevel() 
        {
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                List<string> skillLevel = new List<string> { "Beginner", "Intermediate", "Proficient", "Advanced", "Expert" };

                return skillLevel;
            }
        }
        public ICollection<EmployeeSkill> getEmployeeSkills(SearchEmpSkill skl)
        {
            throw new NotImplementedException();
        }

        //public ICollection<Employee> getMangersEmployee(int managerId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
