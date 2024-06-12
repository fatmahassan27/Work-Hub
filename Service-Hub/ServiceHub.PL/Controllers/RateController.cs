using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork ;

        public RateController(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddRateToWorker(int workerId,int userId, int value, string review)
        {
            try
            {
               await unitOfWork.RateRepo.AddRate(workerId,userId,value,review);
               await unitOfWork.saveAsync();
                return Ok();

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      
    }
}
