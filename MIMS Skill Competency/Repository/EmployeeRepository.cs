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
            using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            {

                string query = "SELECT * FROM Employee";
                return dbConnection.Query<Employee>(query).ToList();
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
                string query = "SELECT * FROM Employee WHERE managerid = @ManagerId";
                return dbConnection.Query<Employee>(query, new { ManagerId = managerId }).ToList();
            }
        }
    }
}
