using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.UnitOfWork;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UnitWork unit;
        string text = "aaa";
        public TestController(UnitWork unit)
        {
            this.unit = unit;            
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(text);
        }
    }
}
