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

        public IEnumerable<string> GetDistinctSkillDomainType(int employeeId)
        {
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                string query = "SELECT distinct(skill_domain_type) FROM skillemployee WHERE emp_id = @empId";
                return dbConnection.Query<string>(query, new { empId = employeeId }).ToList();
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
