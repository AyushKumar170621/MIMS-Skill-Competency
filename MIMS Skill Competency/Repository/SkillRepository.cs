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

        public IEnumerable<SkillDomainType> GetSkillDomainType()
        {
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                string query = "GetAllDomainTypes";
                return dbConnection.Query<SkillDomainType>(query, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public IEnumerable<SkillLevel> GetSkillLevel() 
        {
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                string query = "GetSkillLevelDetails";
                return dbConnection.Query<SkillLevel>(query,commandType:CommandType.StoredProcedure).ToList();
            }
        }

        public ICollection<Skill> getSkillBySkillDomain(List<SkillDomain> skillDomains)
        {
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                var table = new DataTable();
                table.Columns.Add("Item", typeof(int));
                foreach(var sd in skillDomains)
                {
                    table.Rows.Add(sd.DomainId);
                }
                var parameters = new DynamicParameters();
                parameters.Add("@DomainIds", table.AsTableValuedParameter("dbo.IntListType"));
                string query = "GetSkillsByDomainIds";
                var res = dbConnection.Query<Skill>(query,parameters,commandType:CommandType.StoredProcedure).ToList();
                return res;
            }
        }

        public ICollection<SkillDomain> getSkillDomains()
        {
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                string query = "GetAllSkillDomainDetails";
                return dbConnection.Query<SkillDomain>(query,commandType:CommandType.StoredProcedure).ToList();
            }
        }

        //public ICollection<Employee> getMangersEmployee(int managerId)
        //{
        //    throw new NotImplementedException();
        //}

        public ICollection<EmployeeSkill> SearchEmployees(
      //int managerId,
         List<string> employeeNames = null,
         List<string> skillDomainTypes = null,
         List<string> skillDomains = null,
         List<string> skills = null,
         List<string> skillLevels = null,
         string experienceYears = null,
         string experienceMonth = null)
        {
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                string query = "TemporarySearch";

                return dbConnection.Query<EmployeeSkill>(query, commandType: CommandType.StoredProcedure).ToList();
            }

        }

       
    }
}
