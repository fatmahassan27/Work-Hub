using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ServiceHub.BL.DTO;
using ServiceHub.BL.UnitOfWork;
using ServiceHub.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly UnitWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobsController"/> class.
        /// </summary>
        /// <param name="unit">The unit of work.</param>
        public JobsController(UnitWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            Console.WriteLine(User.Identity.Name);
        }

        /// <summary>
        /// Gets all jobs.
        /// </summary>
        /// <returns>A list of JobDTOs.</returns>
        [HttpGet("AllJobs")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var jobs = await unitOfWork.JobRepo.GetAllAsync();
                if(jobs !=null)
                {
                    var JobDTOs = jobs.Select(item => new JobDTO
                    {
                        Id=item.Id,
                        Name = item.Name,
                        Price = item.Price
                    }).ToList();
                    return Ok(JobDTOs);
                }
                return NotFound();
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
        [HttpGet("GetJobById/{id:int}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            try
            {
                var job = await unitOfWork.JobRepo.GetByIdAsync(id);
                if (job != null)
                {
                    var jobDto = JobDTO.FromJob(job);
                    return Ok(jobDto);
                }

                 return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving the jobs.", details = ex.Message });

            }

        }

        /// <summary>
        /// Adds a new job.
        /// </summary>
        /// <param name="jobDto">The job DTO.</param>
        /// <returns>The created JobDTO.</returns>
        [HttpPost]
        public async Task<IActionResult> AddJob(JobDTO jobDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var job = JobDTO.ToJob(jobDto);
                    await unitOfWork.JobRepo.CreateAsync(job); //this function return a task
                    unitOfWork.saveAsync();
                    return Created();
                }
                return BadRequest("Data is Not Valid");
            
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
                var job = await unitOfWork.JobRepo.GetByIdAsync(id);
                if (job != null)
                {
                    await unitOfWork.JobRepo.DeleteAsync(id);
                    unitOfWork.saveAsync();
                    return Ok("Row Deleted Successfully");
                }               
                return NotFound();
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
        [HttpPut("EditByJobId{id:int}")]
        public async Task<IActionResult> Edit(int jobId, [FromBody]JobDTO jobDTO)
        {
            try
            {
                var job = await unitOfWork.JobRepo.GetByIdAsync(jobId);
                if (job != null)
                {
                    
                    job.Name = jobDTO.Name;
                    job.Price = jobDTO.Price;
                    var result= unitOfWork.JobRepo.UpdateAsync(job.Id,job);
                    if(result !=null )
                    {
                        unitOfWork.saveAsync();
                        return Ok("Updated");
                    }
                  
                }
                return NotFound();

            }
       
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the job.", details = ex.Message });
            }
        }

    }
}
