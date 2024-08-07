using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployee();
        IEnumerable<Employee> GetEmployeeById(int id);
        IEnumerable<Employee> getMangersEmployee(int managerId);
    }
}