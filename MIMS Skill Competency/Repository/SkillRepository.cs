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

                    // Return the results (including an empty list if no domain types are found)
                    return result;
                }
            }
            catch (Exception ex)
            {
                // Log the exception details here
                // For example: _logger.LogError(ex, "An error occurred while fetching skill domain types.");

                // Rethrow or handle the exception based on your error handling strategy
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

                    // Execute the query and return the results
                    var result = dbConnection.Query<SkillLevel>(query, commandType: CommandType.StoredProcedure).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Log the exception details here
                // For example: _logger.LogError(ex, "An error occurred while fetching skill levels.");

                // Rethrow or handle the exception based on your error handling strategy
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

                    // Execute the query and return the results
                    var result = dbConnection.Query<SkillDomain>(query, commandType: CommandType.StoredProcedure).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Log the exception details here
                // For example: _logger.LogError(ex, "An error occurred while fetching skill domains.");

                // Rethrow or handle the exception based on your error handling strategy
                throw new ApplicationException("An error occurred while fetching skill domains.", ex);
            }
        }

        //public ICollection<Employee> getMangersEmployee(int managerId)
        //{
        //    throw new NotImplementedException();
        //}

        //public ICollection<EmployeeSkill> SearchEmployees(
        // //int managerId,
        // List<string> employeeNames = null,
        // List<string> skillDomainTypes = null,
        // List<string> skillDomains = null,
        // List<string> skills = null,
        // List<string> skillLevels = null,
        // string experienceYears = null,
        // string experienceMonth = null)
        //{
        //    using (IDbConnection dbConnection = _dbcontext.CreateConnection())
        //    {

        //        //var parameters = new DynamicParameters();
        //        //parameters.Add("@DomainIds", table.AsTableValuedParameter("dbo.IntListType"));


        //        string query = "GetSkillEmployeeDetails1";

        //        return dbConnection.Query<EmployeeSkill>(query, commandType: CommandType.StoredProcedure).ToList();
        //    }

        //}

        public ICollection<EmployeeSkill> SearchEmployees(
        List<int> employeeIds,
        List<int> skillDomainTypes,
        List<int> skillDomains,
        List<int> skills,
        List<int> skillLevels,
        int? experienceYears = null, // Nullable int
        int? experienceMonth = null) // Nullable int
        {
            //var a = employeeIds;
            //Console.WriteLine(a);
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

                    // Function to convert List<int> to DataTable
                    

                    // Add table-valued parameters
                    //parameters.Add("@SEmployeeId", ConvertToDataTable(employeeIds).AsTableValuedParameter("dbo.IntListType"));
                    parameters.Add("@EmployeeId", ConvertToDataTable(employeeIds).AsTableValuedParameter("dbo.IntListType"));
                    parameters.Add("@DomainID", ConvertToDataTable(skillDomains).AsTableValuedParameter("dbo.IntListType"));
                    parameters.Add("@SkillId", ConvertToDataTable(skills).AsTableValuedParameter("dbo.IntListType"));
                    parameters.Add("@LevelId", ConvertToDataTable(skillLevels).AsTableValuedParameter("dbo.IntListType"));
                    parameters.Add("@DomainType", ConvertToDataTable(skillDomainTypes).AsTableValuedParameter("dbo.IntListType"));

                    // Add scalar parameters
                    parameters.Add("@ExpYear", experienceYears.HasValue ? (object)experienceYears.Value : 0, DbType.Int32);
                    parameters.Add("@ExpMonth", experienceMonth.HasValue ? (object)experienceMonth.Value : 0, DbType.Int32);

                    // Execute stored procedure
                    var query = "GetSkillEmployeeDetails1";
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
