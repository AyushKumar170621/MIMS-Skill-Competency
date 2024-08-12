using MIMS_Skill_Competency.Models;
using MIMS_Skill_Competency.Interfaces;
using System.Data;
using Dapper;
using MIMS_Skill_Competency.Data;

namespace MIMS_Skill_Competency.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            try
            {
                using (IDbConnection dbConnection = _dbcontext.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new InvalidOperationException("Unable to create a database connection.");
                    }

                    string query = "GetAllEmployees";
                    var result = dbConnection.Query<Employee>(query, commandType: CommandType.StoredProcedure).ToList();

                    return result ?? new List<Employee>(); // Ensure the result is never null
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving employees.", ex);
            }
        }

        public IEnumerable<Employee> GetEmployeeById(int id)
        {
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                string query = "SELECT * FROM Employee WHERE employeeid = @EmpId";
                return dbConnection.Query<Employee>(query, new { EmpId = id }).ToList();
            }
        }

        public IEnumerable<Employee> getMangersEmployee(int managerId)
        {
            if (managerId <= 0)
            {
                throw new ArgumentException("Invalid Manager ID. It must be a positive integer.", nameof(managerId));
            }

            try
            {
                using (IDbConnection dbConnection = _dbcontext.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new InvalidOperationException("Unable to create a database connection.");
                    }

                    var parameters = new DynamicParameters();
                    parameters.Add("ManagerId", managerId, DbType.Int32);

                    var employees = dbConnection.Query<Employee>("GetEmployeesUnderManager", parameters, commandType: CommandType.StoredProcedure).ToList();

                    if (employees == null || !employees.Any())
                    {
                        // Depending on the calling code, you might want to return an empty list or handle this differently.
                        return new List<Employee>(); // Return an empty list if no employees are found.
                    }

                    return employees;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (replace with your logging mechanism)
                // LogError(ex);

                // Optionally, handle the exception or rethrow it based on your needs.
                // For example, you might rethrow or wrap it in a custom exception:
                throw new ApplicationException("An error occurred while retrieving employees.", ex);
            }
        }
    }
}
