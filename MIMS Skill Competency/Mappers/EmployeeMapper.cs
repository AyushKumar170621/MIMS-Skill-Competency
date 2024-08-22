using MIMS_Skill_Competency.Dtos;
using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto ToEmployeeDto(this Employee employeeModel)
        {
            return new EmployeeDto
            {
                employeeid = employeeModel.employeeid,
                firstname = employeeModel.firstname,
                lastname = employeeModel.lastname
            };
        }
    }
}
