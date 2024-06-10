
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService jobService;
        private readonly IMapper mapper;

        public JobsController(IJobService _jobService,IMapper mapper)
        {
            jobService = _jobService;
            this.mapper = mapper;
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
                    var JobDTOs = mapper.Map<IEnumerable<JobDTO>>(jobs);
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
                    var JobDTO = mapper.Map<JobDTO>(job);
                    return Ok(JobDTO);
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
                    var updatedJob = mapper.Map<Job>(jobDTO);
                    jobService.UpdateJob(updatedJob);
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
