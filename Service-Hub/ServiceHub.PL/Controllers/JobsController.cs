using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService jobService;

        public JobsController(IJobService _jobService)
        {
            jobService = _jobService;
        }

        // api/jobs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var jobsDTO = await jobService.GetAllJobs();
                if (jobsDTO != null)
                {
                    return Ok(jobsDTO);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving the jobs.", details = ex.Message });
            }
        }

        // api/jobs/id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            try
            {
                var jobDTO = await jobService.GetJobById(id);

                if (jobDTO != null)
                {
                    return Ok(jobDTO);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving the jobs.", details = ex.Message });

            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, JobDTO jobDTO)
        {
            if(id!=jobDTO.Id) return BadRequest(); 
            try
            {
                 await jobService.UpdateJob(jobDTO);
                 return Ok("Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the job.", details = ex.Message });
            }
        }
    }
}