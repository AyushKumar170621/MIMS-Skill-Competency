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
            var obj = _employeeRepo.GetAllEmployee();
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }


        [HttpGet("employee/{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var obj = _employeeRepo.GetEmployeeById(id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("manager/employee/{managerId}")]
        public ActionResult<IEnumerable<Employee>> getMangersEmployee(int managerId)
        {
            var obj = _employeeRepo.getMangersEmployee(managerId);
            
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("skillDomainType/")]
        public ActionResult<IEnumerable<string>> GetSkillDomainType()
        {
            var skillDomainTypes = _skillRepo.GetSkillDomainType();
            if (skillDomainTypes == null || !skillDomainTypes.Any())
            {
                return NotFound();
            }

            return Ok(skillDomainTypes);
        }

        [HttpGet("skillDomains/")]
        public ActionResult<IEnumerable<SkillDomain>> GetAllSkillDomain() { 
            var skillDom = _skillRepo.getSkillDomains();
            if(skillDom == null || !skillDom.Any())
            {
                return NotFound();
            }
            return Ok(skillDom);
        }

        [HttpPost("DomainSkill/")]
        public ActionResult<Skill> GetSkillByDomain([FromBody] List<SkillDomain> domain)
        {
            var skill = _skillRepo.getSkillBySkillDomain(domain);
            if(skill == null || !skill.Any())
            {
                return NotFound();
            }
            return Ok(skill);
        }
        [HttpGet("skillLevel/")]
        public ActionResult<IEnumerable<string>> GetSkillLevel()
        {
            var skillLevel = _skillRepo.GetSkillLevel();
            if (skillLevel == null || !skillLevel.Any())
            {
                return NotFound();
            }

            return Ok(skillLevel);
        }

        [HttpGet("search")]
        public ActionResult<ICollection<EmployeeSkill>> SearchEmployees(
       
       [FromQuery] List<string> employeeName = null,
       [FromQuery] List<string> skillDomainType = null,
       [FromQuery] List<string> skillDomain = null,
       [FromQuery] List<string> skill = null,
       [FromQuery] List<string> skillLevel = null,
       [FromQuery] string experienceYear = null,
       [FromQuery] string experienceMonth = null)
        {
            var employees = _skillRepo.SearchEmployees(
                
                employeeName,
                skillDomainType,
                skillDomain,
                skill,
                skillLevel,
                experienceYear,
                experienceMonth);

            return Ok(employees);
        }

    }
}
