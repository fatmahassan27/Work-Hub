using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTO;
using ServiceHub.BL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly UnitWork _unit;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobsController"/> class.
        /// </summary>
        /// <param name="unit">The unit of work.</param>
        public JobsController(UnitWork unit)
        {
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        /// <summary>
        /// Gets all jobs.
        /// </summary>
        /// <returns>A list of JobDTOs.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var jobs = await _unit.JobRepo.GetAll();
                var jobsDTO = JobDTO.FromJobs(jobs.ToList());
                return Ok(jobsDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving the jobs.", details = ex.Message });
            }
        }

        /// <summary>
        /// Gets a job by its ID.
        /// </summary>
        /// <param name="id">The job ID.</param>
        /// <returns>The JobDTO.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _unit.JobRepo.GetById(id);

            if (job == null)
            {
                return NotFound();
            }

            var jobDto = JobDTO.FromJob(job);
            return Ok(jobDto);
        }

        /// <summary>
        /// Adds a new job.
        /// </summary>
        /// <param name="jobDto">The job DTO.</param>
        /// <returns>The created JobDTO.</returns>
        [HttpPost]
        public async Task<IActionResult> AddJob(JobDTO jobDto)
        {
            if (jobDto == null)
            {
                return BadRequest("Job data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var job = JobDTO.ToJob(jobDto);
                await _unit.JobRepo.Create(job); //this function return a task
                _unit.saveChanges();
                return Created();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the job.", details = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a job by its ID.
        /// </summary>
        /// <param name="id">The job ID.</param>
        /// <returns>No content if the job was deleted, NotFound if the job does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            try
            {
                var job = await _unit.JobRepo.GetById(id);

                if (job == null)
                {
                    return NotFound();
                }

                await _unit.JobRepo.Delete(id);//this function return a task
                _unit.saveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while deleting the job.", details = ex.Message });
            }
        }
        /// <summary>
        /// Updates a job.
        /// </summary>
        /// <param name="jobDto">The updated job DTO.</param>
        /// <returns>The updated Job entity.</returns>
        [HttpPut]
        public async Task<IActionResult> Edit(JobDTO jobDto)
        {
            if (jobDto == null)
            {
                return BadRequest("Job data is required.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var job = await _unit.JobRepo.GetById(jobDto.Id);
            if (job == null)
            {
                return NotFound();
            }

            // Update the properties of the existing job entity with values from the DTO
            job.Name = jobDto.Name;
            job.Price = jobDto.Price;
            // Update other properties as needed

            try
            {
                var updatedJob = await _unit.JobRepo.Update(job.Id, job);
                _unit.saveChanges();

                return Ok(JobDTO.FromJob(updatedJob));
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the job.", details = ex.Message });
            }
        }

    }
}
