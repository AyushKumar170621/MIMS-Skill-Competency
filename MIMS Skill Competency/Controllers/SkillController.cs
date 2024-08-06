using Microsoft.AspNetCore.Mvc;

namespace MIMS_Skill_Competency.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> getDemo(string skillId)
        {
            List<string> demo = new List<string>();
            demo.Add("hello");
            demo.Add("hello1");
            return Ok(demo);
        }
    }
}
