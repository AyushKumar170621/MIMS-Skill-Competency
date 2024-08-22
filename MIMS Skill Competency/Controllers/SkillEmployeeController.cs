using Dapper;
using Microsoft.AspNetCore.Mvc;
using MIMS_Skill_Competency.Interfaces;
using MIMS_Skill_Competency.Models;
using MIMS_Skill_Competency.Mappers;
using System.Collections.Generic;
using MIMS_Skill_Competency.Dtos;
using AutoMapper;

namespace MIMS_Skill_Competency.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillEmployeeController : Controller
    {
        private readonly ISkillRepository _skillRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;

        public SkillEmployeeController(ISkillRepository skillRepo, IEmployeeRepository employeeRepo, IMapper mapper)
        {
            _skillRepo = skillRepo;
            _employeeRepo = employeeRepo;
            _mapper = mapper;
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

        [HttpGet("employee")]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployees(bool isAdmin, int? managerId = null)
        {
            try
            {
                IEnumerable<EmployeeDto> obj;

                if (isAdmin)
                {
                    obj = _employeeRepo.GetAllEmployee().Select(o => o.ToEmployeeDto());
                }
                else if (managerId.HasValue)
                {
                    obj = _employeeRepo.getMangersEmployee(managerId.Value).Select(o => o.ToEmployeeDto());
                }
                else
                {
                    return BadRequest("Manager ID is required for non-admin users.");
                }

                if (!obj.Any())
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

       


        [HttpGet("skillDomainType/")]
        public ActionResult<IEnumerable<SkillDomainTypeDTO>> GetSkillDomainType()
        {
            try
            {
                var skillDomainTypes = _mapper.Map<List<SkillDomainTypeDTO>>(_skillRepo.GetSkillDomainType());

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
        public ActionResult<IEnumerable<SkillDomainDTO>> GetAllSkillDomain() {
            try
            {
                var skillDom = _mapper.Map<List<SkillDomainDTO>>(_skillRepo.getSkillDomains());
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
        public ActionResult<SkillDTO> GetSkillByDomain([FromBody] List<int> domain)
        {
            try
            {
                var skill = _mapper.Map<List<SkillDTO>>(_skillRepo.getSkillBySkillDomain(domain));
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
        public ActionResult<IEnumerable<SkillLevelDTO>> GetSkillLevel()
        {
            try
            {
                var skillLevel = _mapper.Map<List<SkillLevelDTO>>(_skillRepo.GetSkillLevel());
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
