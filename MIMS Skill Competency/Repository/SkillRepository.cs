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
            try
            {
                using (IDbConnection dbConnection = _dbcontext.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new InvalidOperationException("Failed to create a database connection.");
                    }

                    string query = "GetAllDomainTypes";

                    var result = dbConnection.Query<SkillDomainType>(query, commandType: CommandType.StoredProcedure).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching skill domain types.", ex);
            }
        }

        public IEnumerable<SkillLevel> GetSkillLevel()
        {
            try
            {
                using (IDbConnection dbConnection = _dbcontext.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new InvalidOperationException("Failed to create a database connection.");
                    }

                    string query = "GetSkillLevelDetails";

                    var result = dbConnection.Query<SkillLevel>(query, commandType: CommandType.StoredProcedure).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching skill levels.", ex);
            }
        }

        public ICollection<Skill> getSkillBySkillDomain(List<SkillDomain> skillDomains)
        {
            var table = new DataTable();
            table.Columns.Add("Item", typeof(int));
            foreach (var sd in skillDomains)
            {
                table.Rows.Add(sd.DomainId);
            }
            try
            {
                using (IDbConnection dbConnection = _dbcontext.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new InvalidOperationException("Failed to create a database connection.");
                    }
                    var parameters = new DynamicParameters();
                    parameters.Add("@DomainIds", table.AsTableValuedParameter("dbo.IntListType"));
                    string query = "GetSkillsByDomainIds";
                    var res = dbConnection.Query<Skill>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching skills by domain IDs.", ex);
            }

        }

        public ICollection<SkillDomain> getSkillDomains()
        {
            try
            {
                using (IDbConnection dbConnection = _dbcontext.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new InvalidOperationException("Failed to create a database connection.");
                    }

                    string query = "GetAllSkillDomainDetails";

                    var result = dbConnection.Query<SkillDomain>(query, commandType: CommandType.StoredProcedure).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching skill domains.", ex);
            }
        }


        public ICollection<EmployeeSkill> SearchEmployees(
        List<int> employeeIds,
        List<int> skillDomainTypes,
        List<int> skillDomains,
        List<int> skills,
        List<int> skillLevels,
        int? minExperience = null,
        int? maxExperience = null) 
        {
            DataTable ConvertToDataTable(List<int> list)
            {
                var table = new DataTable();
                table.Columns.Add("Item", typeof(int));
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        table.Rows.Add(item);
                    }
                }
                return table;
            }
            try
            {
                using (IDbConnection dbConnection = _dbcontext.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new InvalidOperationException("Failed to create a database connection.");
                    }
                    var parameters = new DynamicParameters();

                    // Adding table-valued parameters
                    parameters.Add("@EmployeeId", ConvertToDataTable(employeeIds).AsTableValuedParameter("dbo.IntListType"));
                    parameters.Add("@DomainID", ConvertToDataTable(skillDomains).AsTableValuedParameter("dbo.IntListType"));
                    parameters.Add("@SkillId", ConvertToDataTable(skills).AsTableValuedParameter("dbo.IntListType"));
                    parameters.Add("@LevelId", ConvertToDataTable(skillLevels).AsTableValuedParameter("dbo.IntListType"));
                    parameters.Add("@DomainType", ConvertToDataTable(skillDomainTypes).AsTableValuedParameter("dbo.IntListType"));

                    // Adding scalar parameters
                    parameters.Add("@ExpFromYears", minExperience.HasValue ? (object)minExperience.Value : 0, DbType.Int32);
                    parameters.Add("@ExpToYears", maxExperience.HasValue ? (object)maxExperience.Value : 0, DbType.Int32);

                    // Execute stored procedure
                    var query = "GetDetailedSkillEmployeeValid";
                    return dbConnection.Query<EmployeeSkill>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while searching for employees.", ex);
            }
        }

    }
}
