
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.DAL.Interfaces;

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
                var jobs = await jobService.GetAllJobs();
                if (jobs != null)
                {
                    var JobDTOs = jobs.Select(item => new JobDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price
                    }).ToList();
                    return Ok(JobDTOs);
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
                var job = await jobService.GetJobById(id);
                if (job != null)
                {
                    var jobDto = JobDTO.FromJob(job);
                    return Ok(jobDto);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving the jobs.", details = ex.Message });

            }
        }
             
        [HttpPut("{id:int}")] //Review
        public async Task<IActionResult> Edit(int id , JobDTO jobDTO)
        {
            try
            {
                var job = await jobService.GetJobById(id);
                if (job != null)
                {
                    job.Name = jobDTO.Name;
                    job.Price = jobDTO.Price;
                    jobService.UpdateJob(job); 
                    return Ok("Updated");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the job.", details = ex.Message });
            }
        }

    }
}
