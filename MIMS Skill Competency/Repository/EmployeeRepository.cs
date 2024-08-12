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
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {
                var parameter = new DynamicParameters();
                parameter.Add("ManagerId", managerId, DbType.Int32);
                //string query = "SELECT * FROM Employee WHERE managerid = @ManagerId";
                return dbConnection.Query<Employee>("GetEmployeesUnderManager", parameter,commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
