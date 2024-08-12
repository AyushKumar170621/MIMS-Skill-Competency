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
     [FromQuery] List<int> employeeIds = null,
     [FromQuery] List<int> skillDomainTypes = null,
     [FromQuery] List<int> skillDomains = null,
     [FromQuery] List<int> skills = null,
     [FromQuery] List<int> skillLevels = null,
     [FromQuery] int? experienceYears = null, // Nullable int
     [FromQuery] int? experienceMonth = null) // Nullable int
        {
            // Initialize lists if they are null
            employeeIds = employeeIds ?? new List<int>();
            skillDomainTypes = skillDomainTypes ?? new List<int>();
            skillDomains = skillDomains ?? new List<int>();
            skills = skills ?? new List<int>();
            skillLevels = skillLevels ?? new List<int>();

            // Call the repository method with the parameters
            var employees = _skillRepo.SearchEmployees(
                employeeIds,
                skillDomainTypes,
                skillDomains,
                skills,
                skillLevels,
                experienceYears,
                experienceMonth);

            return Ok(employees);
        }




    }
}
