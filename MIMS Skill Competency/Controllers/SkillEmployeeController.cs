using Dapper;
using Microsoft.AspNetCore.Mvc;
using MIMS_Skill_Competency.Interfaces;
using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillEmployeeController : Controller
    {
        private readonly ISkillRepository _skillRepo;
        private readonly IEmployeeRepository _employeeRepo;

        public SkillEmployeeController(ISkillRepository skillRepo, IEmployeeRepository employeeRepo)
        {
            _skillRepo = skillRepo;
            _employeeRepo = employeeRepo;
        }

        [HttpGet("employees")]
        public ActionResult<IEnumerable<Employee>> GetAllEmployee()
        {
            try
            {
                var obj = _employeeRepo.GetAllEmployee();
                if (obj.Count() == 0)
                {
                    return NotFound("No records found.");
                }
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("employee/{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            try
            {
                var obj = _employeeRepo.GetEmployeeById(id);
                if (obj.Count() == 0)
                {
                    return NotFound("No record found with this employee id.");
                }
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("manager/employee/{managerId}")]
        public ActionResult<IEnumerable<Employee>> getMangersEmployee(int managerId)
        {
            try
            {
                var obj = _employeeRepo.getMangersEmployee(managerId);
                if (obj.Count() == 0)
                {
                    return NotFound("No record found with this manager id.");
                }
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

   
        [HttpGet("skillDomainType/")]
        public ActionResult<IEnumerable<string>> GetSkillDomainType()
        {
            try
            {
                var skillDomainTypes = _skillRepo.GetSkillDomainType();

                if (skillDomainTypes.Count() == 0 || !skillDomainTypes.Any())
                {
                    return NotFound("No record found.");
                }

                return Ok(skillDomainTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("skillDomains/")]
        public ActionResult<IEnumerable<SkillDomain>> GetAllSkillDomain() {
            try
            {
                var skillDom = _skillRepo.getSkillDomains();
                if (skillDom.Count() == 0 || !skillDom.Any())
                {
                    return NotFound("No record found.");
                }
                return Ok(skillDom);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("DomainSkill/")]
        public ActionResult<Skill> GetSkillByDomain([FromBody] List<SkillDomain> domain)
        {
            try
            {
                var skill = _skillRepo.getSkillBySkillDomain(domain);
                if (skill.Count() == 0 || !skill.Any())
                {
                    return NotFound("No record found for the provided skill domains.");
                }
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("skillLevel/")]
        public ActionResult<IEnumerable<string>> GetSkillLevel()
        {
            try
            {
                var skillLevel = _skillRepo.GetSkillLevel();
                if (skillLevel.Count() == 0 || !skillLevel.Any())
                {
                    return NotFound("No record found.");
                }

                return Ok(skillLevel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("search")]
        public ActionResult<ICollection<EmployeeSkill>> SearchEmployees([FromBody] SearchEmpSkill searchCriteria)
        {
           

            try
            {
               
                var employees = _skillRepo.SearchEmployees(
                    searchCriteria.EmployeeIds,
                    searchCriteria.SkillDomainTypes,
                    searchCriteria.SkillDomains,
                    searchCriteria.Skills,
                    searchCriteria.SkillLevels,
                    searchCriteria.MinExperience,
                    searchCriteria.MaxExperience);

                if (employees == null)
                {
                    return NotFound();
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
