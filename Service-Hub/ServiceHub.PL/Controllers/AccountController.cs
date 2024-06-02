using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServiceHub.BL.UnitOfWork;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UnitWork unitWork;

        public AccountController(UnitWork unitWork) 
        {
            this.unitWork = unitWork;
        }
        //[HttpGet]
        //public IActionResult Registration()
        //{

        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Registration()
        //{

        //}
    }
}
