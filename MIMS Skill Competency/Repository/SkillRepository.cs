using Dapper;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<string> GetDistinctSkillDomainType(int employeeId)
        {
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                string query = "SELECT distinct(skill_domain_type) FROM skillemployee WHERE emp_id = @empId";
                return dbConnection.Query<string>(query, new { empId = employeeId }).ToList();
            }
        }

        public ICollection<Skill> getSkillBySkillDomain(List<SkillDomain> skillDomains)
        {
            var skill = new List<Skill>();
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                foreach (var skillDomain in skillDomains)
                {
                    string query = "SELECT SkillId,SkillName FROM skill where DomainId = "+skillDomain.DomainId;
                    var res = dbConnection.Query<Skill>(query).ToList();
                    skill.AddRange(res);
                }
            }
            return skill;
        }

        public ICollection<SkillDomain> getSkillDomains()
        {
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                string query = "SELECT DomainId,DomainName FROM skillDomain";
                return dbConnection.Query<SkillDomain>(query).ToList();
            }
        }

        //public ICollection<Employee> getMangersEmployee(int managerId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
