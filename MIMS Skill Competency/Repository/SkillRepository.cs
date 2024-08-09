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

        public ICollection<SearchEmpSkill> SearchEmployees(
      //int managerId,
         List<string> employeeNames = null,
         List<string> skillDomainTypes = null,
         List<string> skillDomains = null,
         List<string> skills = null,
         List<string> skillLevels = null,
         string experienceYears = null,
         string experienceMonth = null)
        {

            if (employeeNames != null && employeeNames.Any())
            {
                Console.WriteLine("name ok");
            }
            if (skillDomainTypes != null && skillDomainTypes.Any())
            {
                Console.WriteLine("domainType ok");
            }
            if (skillDomains != null && skillDomains.Any())
            {
                Console.WriteLine("skillDomain ok");
            }
            if (skills != null && skills.Any())
            {
                Console.WriteLine("skills ok");
            }
            if (skillLevels != null && skillLevels.Any())
            {
                Console.WriteLine("skillLevel ok");
            }
            if (experienceYears != null && experienceYears.Any())
            {
                Console.WriteLine("year ok");
            }
            if (experienceMonth != null && experienceMonth.Any())
            {
                Console.WriteLine("month ok");
            }

            // Sample data for demonstration
            var sampleData = new List<SearchEmpSkill>
          {
              new SearchEmpSkill
              {
                  Employees = new List<Employee>
                  {
                      new Employee { employeeid = 1, firstname = "John Doe" },
                      new Employee { employeeid = 2, firstname = "Jane Smith" }
                  },
                  DomainType = new List<string> { "IT", "HR" },
                  SkillDomain = new List<string> { "Development", "Management" },
                  Skill = new List<string> { "Web Development", "Project Management" },
                  SkillLevel = new List<string> { "Intermediate", "Expert" },
                  ExperienceMonth = 12,
                  ExperienceYear = 5
              },
              new SearchEmpSkill
              {
                  Employees = new List<Employee>
                  {
                      new Employee {employeeid = 3,firstname = "Alice Johnson"}
                  },
                  DomainType = new List<string> { "Finance" },
                  SkillDomain = new List<string> { "Accounting" },
                  Skill = new List<string> { "Financial Analysis" },
                  SkillLevel = new List<string> { "Beginner" },
                  ExperienceMonth = 6,
                  ExperienceYear = 2
              }
          };

            // Uncomment the following code when connecting to the database
            // using (IDbConnection dbConnection = _dbcontext.CreateConnection())
            // {
            //     var sqlQuery = new StringBuilder(
            //         "SELECT emp.emp_id, emp.emp_name, semp.skill_domain_type, semp.skill_domain, semp.skill, semp.skill_level, semp.experience_year " +
            //         "FROM employee emp " +
            //         "JOIN skillemployee semp ON emp.emp_id = semp.emp_id " +
            //         "WHERE emp.manager_id = @ManagerId");
            //
            //     var parameters = new DynamicParameters();
            //     parameters.Add("@ManagerId", managerId);
            //
            //     if (employeeNames != null && employeeNames.Any())
            //     {
            //         sqlQuery.Append(" AND emp.emp_name IN @EmployeeNames");
            //         parameters.Add("@EmployeeNames", employeeNames);
            //     }
            //     if (skillDomainTypes != null && skillDomainTypes.Any())
            //     {
            //         sqlQuery.Append(" AND semp.skill_domain_type IN @SkillDomainTypes");
            //         parameters.Add("@SkillDomainTypes", skillDomainTypes);
            //     }
            //     if (skillDomains != null && skillDomains.Any())
            //     {
            //         sqlQuery.Append(" AND semp.skill_domain IN @SkillDomains");
            //         parameters.Add("@SkillDomains", skillDomains);
            //     }
            //     if (skills != null && skills.Any())
            //     {
            //         sqlQuery.Append(" AND semp.skill IN @Skills");
            //         parameters.Add("@Skills", skills);
            //     }
            //     if (skillLevels != null && skillLevels.Any())
            //     {
            //         sqlQuery.Append(" AND semp.skill_level IN @SkillLevels");
            //         parameters.Add("@SkillLevels", skillLevels);
            //     }
            //     if (experienceYears != null && experienceYears.Any())
            //     {
            //         sqlQuery.Append(" AND semp.experience_year IN @ExperienceYears");
            //         parameters.Add("@ExperienceYears", experienceYears);
            //     }
            //     if (experienceMonth != null && experienceMonth.Any())
            //     {
            //         sqlQuery.Append(" AND semp.experience_month IN @ExperienceMonth");
            //         parameters.Add("@ExperienceMonth", experienceMonth);
            //     }
            //
            //     return dbConnection.Query<SearchEmpSkill>(sqlQuery.ToString(), parameters).ToList();
            // }

            return sampleData;
        }

       
    }
}
