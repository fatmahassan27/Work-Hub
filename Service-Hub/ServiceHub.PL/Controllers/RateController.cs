using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.UnitOfWork;


namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateService rateService;

        public RateController(IRateService rateService) 
        {
            this.rateService = rateService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRate(RateDTO rateDTO)
        {
            try
            {
                await rateService.AddRate(rateDTO);
                return Ok("Rate Is Added Successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      
        [HttpGet("Average/{id:int}")]
        public async Task<IActionResult> GetRating(int id)
        {
            var data = await rateService.getAverageWorkerRating(id);
            return Ok(data);
        }
    }
}
