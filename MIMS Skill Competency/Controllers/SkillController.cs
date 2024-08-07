using Microsoft.AspNetCore.Mvc;
using MIMS_Skill_Competency.Interfaces;
using MIMS_Skill_Competency.Models;

namespace MIMS_Skill_Competency.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : Controller
    {
        private readonly ISkillRepository _skillRepo;
        private readonly IEmployeeRepository _employeeRepo;

        public SkillController(ISkillRepository skillRepo, IEmployeeRepository employeeRepo)
        {
            _skillRepo = skillRepo;
            _employeeRepo = employeeRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployee()
        {
            var obj = _employeeRepo.GetAllEmployee();
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }


        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var obj = _employeeRepo.GetEmployeeById(id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("manager/{managerId}")]
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

    }
}
